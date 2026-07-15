using UnityEngine;

/// <summary>
/// Minimalist energy resource used to gate the player's shooting.
/// Energy is spent per shot, only restored via AddEnergy (energy cell pickups),
/// and enforces a cooldown between shots. Exposes a normalized value for a future HUD.
/// </summary>
public class EnergySystem : MonoBehaviour
{
    [Header("Energy")]
    [Tooltip("Maximum energy the player can store.")]
    public float maxEnergy = 100f;
    [Tooltip("Current energy. Auto-filled to maxEnergy on Awake.")]
    public float currentEnergy;
    [Tooltip("Energy consumed by a single shot.")]
    public float energyCostPerShot = 10f;
    [Tooltip("Minimum seconds between two shots (caps fire rate).")]
    public float cooldownBetweenShots = 0.2f;

    // Start well in the past so the very first shot is never blocked by cooldown.
    private float lastShotTime = -Mathf.Infinity;

    private void Awake()
    {
        currentEnergy = maxEnergy;
    }

    /// <summary>True when there is enough energy AND the cooldown has elapsed.</summary>
    public bool CanShoot()
    {
        return currentEnergy >= energyCostPerShot
            && Time.time - lastShotTime >= cooldownBetweenShots;
    }

    /// <summary>Spends energy for a shot and starts the cooldown timer.</summary>
    public void UseEnergy(float amount)
    {
        currentEnergy = Mathf.Max(currentEnergy - amount, 0f);
        lastShotTime = Time.time;
    }

    /// <summary>Returns current energy as a 0..1 fraction (for a HUD bar later).</summary>
    public float GetEnergyPercent()
    {
        return maxEnergy > 0f ? currentEnergy / maxEnergy : 0f;
    }

    public void AddEnergy(float amount)
    {
        currentEnergy = Mathf.Min(currentEnergy + amount, maxEnergy);
        Debug.Log($"Energy restored +{amount}. Energy: {currentEnergy}/{maxEnergy}");
    }

    public float GetCurrentEnergy()
    {
        return currentEnergy;
    }

    public float GetMaxEnergy()
    {
        return maxEnergy;
    }
}
