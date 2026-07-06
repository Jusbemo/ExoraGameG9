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

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        patrol = GetComponent<EnemyPatrol>();
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    /// <summary>Invoked via SendMessage from Projectile.cs on impact.</summary>
    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        Debug.Log("Enemy hit! Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
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
