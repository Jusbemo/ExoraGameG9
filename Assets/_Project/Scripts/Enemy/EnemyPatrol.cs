using UnityEngine;

/// <summary>
/// Simple ground patrol: moves back and forth between two waypoints,
/// flipping the sprite to face its travel direction.
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Points (world space)")]
    [SerializeField] private Vector2 waypointA = new Vector2(2f, -1f);
    [SerializeField] private Vector2 waypointB = new Vector2(6f, -1f);

    [Header("Movement")]
    [SerializeField] private float speed = 2f;
    [Tooltip("How close to a waypoint counts as 'arrived'.")]
    [SerializeField] private float arriveThreshold = 0.05f;

    private Vector2 target;
    private int facing;   // 0 = unset, 1 = right, -1 = left

    /// <summary>Current travel direction: 1 = right, -1 = left, 0 = not yet determined.</summary>
    public int Facing => facing;

    private void Start()
    {
        // Begin by heading toward waypoint B (to the right).
        target = waypointB;
    }

    private void Update()
    {
        Vector2 current = transform.position;
        Vector2 next = Vector2.MoveTowards(current, target, speed * Time.deltaTime);
        transform.position = new Vector3(next.x, next.y, transform.position.z);

        UpdateFacing(target.x - current.x);

        if (Vector2.Distance(next, target) <= arriveThreshold)
        {
            target = (target == waypointA) ? waypointB : waypointA;
        }
    }

    /// <summary>Flips to face travel direction and logs only on a change.</summary>
    private void UpdateFacing(float deltaX)
    {
        int direction = deltaX > 0f ? 1 : (deltaX < 0f ? -1 : facing);
        if (direction == 0 || direction == facing) return;
        facing = direction;

        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * direction;
        transform.localScale = scale;

        Debug.Log(direction > 0 ? "Enemy patrolling right" : "Enemy patrolling left");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(waypointA, 0.15f);
        Gizmos.DrawWireSphere(waypointB, 0.15f);
        Gizmos.DrawLine(waypointA, waypointB);
    }
}
