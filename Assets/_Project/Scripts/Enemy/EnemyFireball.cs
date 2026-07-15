using UnityEngine;

/// <summary>
/// Straight-line fireball fired by an enemy Sentinel.
/// Travels along a fixed direction at a constant speed, damages a
/// GameObject tagged "Player" on contact, and self-destructs on impact
/// or after its lifetime expires. Requires a trigger collider and a
/// gravity-free Rigidbody2D.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyFireball : MonoBehaviour
{
    [Header("Ballistics")]
    [Tooltip("Travel speed in units per second.")]
    public float speed = 5f;
    [Tooltip("Seconds the fireball lives before self-destructing if it hits nothing.")]
    public float lifetime = 3f;
    [Tooltip("Damage dealt to the player on impact.")]
    public int damage = 1;

    private Vector2 direction = Vector2.right; // Default so it still moves if SetDirection is skipped.
    private float spawnTime;
    private bool isHit;                          // Guards against double-processing a single hit.
    private Rigidbody2D rb;

    private void Awake()
    {
        // Cache the Rigidbody2D once (never GetComponent in Update).
        rb = GetComponent<Rigidbody2D>();
        spawnTime = Time.time;
    }

    /// <summary>Sets the travel direction and orients the sprite to face it.</summary>
    public void SetDirection(Vector2 dir)
    {
        direction = dir.sqrMagnitude > 0.0001f ? dir.normalized : Vector2.right;

        // Mirror the sprite so it points the way it travels.
        if (direction.x < 0f)
        {
            Vector3 scale = transform.localScale;
            scale.x = -Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
    }

    private void Update()
    {
        if (isHit) return;

        // Constant linear velocity every frame (no AddForce) for a clean straight line.
        rb.velocity = direction * speed;

        // Self-destruct once the lifetime elapses (frame-rate independent).
        if (Time.time - spawnTime >= lifetime)
        {
            Debug.Log("FireBall destroyed - lifetime expired");
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isHit) return;

        if (collision.CompareTag("Player"))
        {
            isHit = true;
            collision.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
            Debug.Log("FireBall hit player!");
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Platform"))
        {
            isHit = true;
            Destroy(gameObject);
        }
    }
}
