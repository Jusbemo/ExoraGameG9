using UnityEngine;

/// <summary>
/// Straight-line projectile fired by the player.
/// Travels along a fixed direction at a constant speed, damages GameObjects
/// tagged "Enemy" on contact, and self-destructs on impact or after its
/// lifetime expires. Requires a trigger collider and a gravity-free Rigidbody2D.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [Header("Ballistics")]
    [Tooltip("Travel speed in units per second.")]
    public float speed = 15f;
    [Tooltip("Seconds the projectile lives before self-destructing if it hits nothing.")]
    public float lifetime = 1.2f;
    [Tooltip("Damage dealt to an enemy on impact.")]
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
            Debug.Log("Projectile destroyed - lifetime expired");
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isHit) return;

        if (collision.CompareTag("Enemy"))
        {
            isHit = true;
            // Enemies don't implement TakeDamage yet; DontRequireReceiver avoids errors for now.
            collision.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
            Debug.Log("Projectile hit enemy: " + collision.name);
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Platform"))
        {
            isHit = true;
            Destroy(gameObject);
        }
    }
}
