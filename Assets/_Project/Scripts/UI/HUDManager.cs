using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDManager : MonoBehaviour
{
    [Header("Player References")]
    public PlayerHealth playerHealth;
    public EnergySystem energySystem;

    [Header("Health Bar")]
    public Image[] healthSegments = new Image[10];

    [Header("Energy Bar")]
    public Image[] energySegments = new Image[10];

    [Header("Text")]
    public TMP_Text componentCounterText;
    public TMP_Text objectiveText;

    private static readonly Color HEALTH_ACTIVE = new Color(0.388f, 0.874f, 0.306f, 1f);
    private static readonly Color HEALTH_INACTIVE = new Color(0.016f, 0.263f, 0.333f, 0.4f);
    private static readonly Color ENERGY_ACTIVE = new Color(0.910f, 0.607f, 0.235f, 1f);
    private static readonly Color ENERGY_INACTIVE = new Color(0.016f, 0.263f, 0.333f, 0.4f);

    private void Start()
    {
        GameManager.Instance.OnComponentCollected += UpdateComponentCounter;
        GameManager.Instance.OnObjectiveChanged += UpdateObjective;

        UpdateComponentCounter();
        UpdateObjective();
    }

    private void Update()
    {
        UpdateHealthBar();
        UpdateEnergyBar();
    }

    private void UpdateHealthBar()
    {
        int currentSegments = playerHealth.GetCurrentHealth();

        for (int i = 0; i < healthSegments.Length; i++)
        {
            healthSegments[i].color = i < currentSegments ? HEALTH_ACTIVE : HEALTH_INACTIVE;
        }
    }

    private void UpdateEnergyBar()
    {
        float energyPercent = energySystem.GetCurrentEnergy() / energySystem.GetMaxEnergy();
        int activeSegments = Mathf.RoundToInt(energyPercent * 10);

        for (int i = 0; i < energySegments.Length; i++)
        {
            energySegments[i].color = i < activeSegments ? ENERGY_ACTIVE : ENERGY_INACTIVE;
        }
    }

    private void UpdateComponentCounter()
    {
        componentCounterText.text = $"{GameManager.Instance.GetComponentsCollected()} / {GameManager.Instance.GetTotalComponents()}";
    }

    private void UpdateObjective(string _ = null)
    {
        objectiveText.text = GameManager.Instance.currentObjective;
    }

    private void OnDestroy()
    {
        if (GameManager.Instance == null) return;
        GameManager.Instance.OnComponentCollected -= UpdateComponentCounter;
        GameManager.Instance.OnObjectiveChanged -= UpdateObjective;
    }
}
