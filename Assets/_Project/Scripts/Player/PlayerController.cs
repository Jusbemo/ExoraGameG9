using UnityEngine;

/// <summary>
/// Side-scroller controller for KAERON VEX.
/// Handles horizontal movement (WASD / arrow keys), a single grounded jump,
/// sprite facing flip, raycast-based ground detection, and animation.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 10f;

    [Header("Ground Check")]
    [Tooltip("Distance below the collider used to detect the ground.")]
    [SerializeField] private float groundCheckDistance = 0.1f;

    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private Animator animator;
    private bool isGrounded;
    private int facing = 1;            // 1 = right, -1 = left
    private int lastLoggedDirection;   // 0 = idle, prevents per-frame log spam

    private void Awake()
    {
       
        rb = GetComponent<Rigidbody2D>();
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        animator.speed = 1f;
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");

        // --- Horizontal movement ---
        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);

        // --- Animation: Update "Running" parameter ---
        bool isMoving = !Mathf.Approximately(horizontal, 0f);
        animator.SetBool("Running", isMoving);

        if (isMoving)
        {
            int direction = horizontal > 0f ? 1 : -1;
            Flip(direction);

            // Log once per direction change rather than every frame.
            if (direction != lastLoggedDirection)
            {
                Debug.Log("Player moving: " + (direction > 0 ? "right" : "left"));
                lastLoggedDirection = direction;
            }
        }
        else
        {
            lastLoggedDirection = 0;
        }

        // --- Ground detection + jump ---
        isGrounded = IsGrounded();

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            Debug.Log("Player Jumping");
        }
    }

    /// <summary>Flips the sprite by mirroring localScale.x (left = -1, right = 1).</summary>
    private void Flip(int direction)
    {
        if (direction == facing) return;
        facing = direction;
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * direction;
        transform.localScale = scale;
    }

    /// <summary>Casts a short ray straight down from the collider's bottom edge.</summary>
    private bool IsGrounded()
    {
        // Ignore our own collider so the cast only reports actual ground.
        bool previous = Physics2D.queriesStartInColliders;
        Physics2D.queriesStartInColliders = false;

        Vector2 origin = new Vector2(boxCollider.bounds.center.x, boxCollider.bounds.min.y);
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, groundCheckDistance);

        Physics2D.queriesStartInColliders = previous;
        return hit.collider != null;
    }

    private void OnDrawGizmosSelected()
    {
        BoxCollider2D col = GetComponent<BoxCollider2D>();
        if (col == null) return;
        Vector2 origin = new Vector2(col.bounds.center.x, col.bounds.min.y);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(origin, origin + Vector2.down * groundCheckDistance);
    }
}