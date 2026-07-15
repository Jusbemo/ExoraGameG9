using UnityEngine;

/// <summary>
/// Health, damage, and death handling for the player.
/// Damage arrives via SendMessage("TakeDamage", ...) from enemy attacks
/// (e.g. EnemyFireball.cs).
/// </summary>
public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth = 10;

    [Header("Fall Death")]
    [Tooltip("Below the lowest platform (-4.5) with a buffer so death only triggers once the player has fully missed every platform, not while still visible near an edge.")]
    public float fallDeathY = -10f;

    private int currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (transform.position.y < fallDeathY)
        {
            Debug.Log("[PlayerHealth] Player fell out of bounds - respawning");
            PlayerDied();
        }
    }

    /// <summary>Invoked via SendMessage from enemy attacks on impact.</summary>
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Player hit! Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            PlayerDied();
        }
    }

    private void PlayerDied()
    {
        Debug.Log("[PlayerHealth] Player died - respawning");
        Respawn();
    }

    private void Respawn()
    {
        Vector3 respawnPos = GameManager.Instance != null
            ? GameManager.Instance.GetRespawnPosition()
            : transform.position;
        transform.position = respawnPos;

        currentHealth = maxHealth;
        Debug.Log($"[PlayerHealth] Respawned at {respawnPos}. Health restored to {currentHealth}/{maxHealth}");

        EnergySystem energy = GetComponent<EnergySystem>();
        if (energy != null)
        {
            energy.AddEnergy(energy.GetMaxEnergy());
            Debug.Log("[PlayerHealth] Energy restored on respawn");
        }

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
        }
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        Debug.Log($"Player healed +{amount}. Health: {currentHealth}/{maxHealth}");
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }
}
