using System.Collections;
using UnityEngine;

/// <summary>
/// Health, damage, and death handling for an enemy.
/// Damage arrives via SendMessage("TakeDamage", ...) from Projectile.cs.
/// On death it stops patrol, plays the "Death" animation, and destroys the
/// GameObject once the clip has finished.
/// </summary>
[RequireComponent(typeof(Animator))]
public class EnemyHealth : MonoBehaviour
{
    [Header("Health")]
    [Tooltip("Hits required to kill this enemy (adjustable per instance).")]
    public int maxHealth = 3;

    private int currentHealth;
    private bool isDead = false;                 // Prevents processing damage/death twice.
    private Animator animator;                   // Cached in Awake.
    private SpriteRenderer spriteRenderer;       // Cached (for optional fade-out later).
    private EnemyPatrol patrol;                  // Cached so we can stop movement on death.
    private Rigidbody2D rb;                      // Cached so we can freeze the body on death.

    [Header("Hit Flash")]
    [Tooltip("Total time the hit flash lasts.")]
    [SerializeField] private float flashDuration = 0.2f;
    [Tooltip("On/off toggle interval during the flash (smaller = faster blinking).")]
    [SerializeField] private float flashInterval = 0.05f;
    [Tooltip("Tint applied on each flash 'on' frame.")]
    [SerializeField] private Color flashColor = new Color(1f, 0.3f, 0.3f, 1f);

    private Color originalColor = Color.white;   // Cached sprite color, restored after each flash.
    private bool isFlashing = false;             // Prevents overlapping flash coroutines.

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        patrol = GetComponent<EnemyPatrol>();
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;

        if (spriteRenderer != null) originalColor = spriteRenderer.color;
    }

    /// <summary>Invoked via SendMessage from Projectile.cs on impact.</summary>
    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;

        // Visual feedback: brief red flash on each hit (skipped if one is already running).
        if (!isFlashing && spriteRenderer != null) StartCoroutine(FlashHit());

        Debug.Log("Enemy hit! Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// Flashes the sprite between its original color and <see cref="flashColor"/>
    /// in on/off pulses for <see cref="flashDuration"/> seconds, then restores it.
    /// </summary>
    private IEnumerator FlashHit()
    {
        isFlashing = true;
        float elapsedTime = 0f;

        while (elapsedTime < flashDuration)
        {
            // Alternate on/off each interval: on for [0,1), off for [1,2), repeating.
            bool on = (elapsedTime / flashInterval) % 2f < 1f;
            spriteRenderer.color = on ? flashColor : originalColor;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Always restore the exact original color when finished.
        spriteRenderer.color = originalColor;
        isFlashing = false;
    }

    private void Die()
    {
        isDead = true;

        // Stop patrol movement without destroying the object yet.
        if (patrol != null) patrol.enabled = false;

        // Freeze the body so the dying enemy doesn't fall/slide (it's a Dynamic
        // rigidbody that patrol was holding in place each frame).
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Kinematic;
        }

        // Triggers are auto-consumed, so no Reset needed.
        if (animator != null) animator.SetTrigger("Death");

        Debug.Log("Enemy died");

        StartCoroutine(WaitForDeathAnimation());
    }

    /// <summary>
    /// Waits the length of the death clip, then destroys the GameObject.
    /// The clip length is read from the controller (reliable), rather than from
    /// GetCurrentAnimatorStateInfo immediately after SetTrigger (which still
    /// reports the previous state until the transition is evaluated).
    /// </summary>
    private IEnumerator WaitForDeathAnimation()
    {
        float animLength = 0.5f; // Fallback if the clip can't be resolved.
        if (animator != null && animator.runtimeAnimatorController != null)
        {
            foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
            {
                if (clip.name == "AlienDeath")
                {
                    animLength = clip.length;
                    break;
                }
            }
        }

        Debug.Log("Death animation length: " + animLength);
        yield return new WaitForSeconds(animLength);

        Destroy(gameObject);
        Debug.Log("Enemy GameObject destroyed");
    }
}
