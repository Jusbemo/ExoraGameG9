using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Health, damage, and death handling for the player.
/// Damage arrives via SendMessage("TakeDamage", ...) from enemy attacks
/// (e.g. EnemyFireball.cs).
/// </summary>
public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth = 10;

    private int currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
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
        Debug.Log("Player died!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
