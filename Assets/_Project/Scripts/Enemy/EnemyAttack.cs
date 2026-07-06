using UnityEngine;

/// <summary>
/// Fires an EnemyFireball at a regular interval, aimed in the direction
/// the attached EnemyPatrol is currently facing, but only once the player
/// has come within detection range.
/// </summary>
public class EnemyAttack : MonoBehaviour
{
    [Header("Fireball")]
    [Tooltip("Fireball prefab spawned when this enemy shoots.")]
    public GameObject fireballPrefab;
    [Tooltip("Seconds between shots.")]
    public float shootInterval = 1f;
    [Tooltip("Spawn position relative to the enemy, mirrored by facing direction.")]
    public Vector2 shootOffset = new Vector2(0.7f, 0f);

    [Header("Detection")]
    [Tooltip("The player must be within this distance for the enemy to shoot.")]
    public float detectionRange = 5f;

    private float lastShotTime = Mathf.NegativeInfinity; // Allows an immediate first shot.
    private int facingDirection = 1;                     // 1 = right, -1 = left
    private EnemyPatrol patrolScript;
    private Transform playerTransform;                    // Cached lookup for the range check.

    private void Awake()
    {
        patrolScript = GetComponent<EnemyPatrol>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) playerTransform = player.transform;
    }

    private void Update()
    {
        if (Time.time - lastShotTime >= shootInterval && PlayerInRange())
        {
            ShootFireball();
        }
    }

    /// <summary>True once the player exists and is within detectionRange.</summary>
    private bool PlayerInRange()
    {
        if (playerTransform == null) return false;
        return Vector2.Distance(transform.position, playerTransform.position) <= detectionRange;
    }

    private void ShootFireball()
    {
        if (fireballPrefab == null)
        {
            Debug.LogWarning("EnemyAttack: fireballPrefab is not assigned.");
            return;
        }

        // Follow the patrol's current travel direction when available.
        if (patrolScript != null && patrolScript.Facing != 0)
        {
            facingDirection = patrolScript.Facing;
        }

        Vector3 spawnPosition = transform.position + (Vector3)(shootOffset * facingDirection);

        GameObject fireballObject = Instantiate(fireballPrefab, spawnPosition, Quaternion.identity);
        EnemyFireball fireball = fireballObject.GetComponent<EnemyFireball>();
        if (fireball != null)
        {
            fireball.SetDirection(new Vector2(facingDirection, 0f));
        }

        lastShotTime = Time.time;
        Debug.Log("Enemy shot fireball! Direction: " + facingDirection);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
