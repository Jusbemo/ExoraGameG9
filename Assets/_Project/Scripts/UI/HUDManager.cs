using System.Collections;
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

    private Vector3 objectiveBaseScale = Vector3.one;
    private Vector3 counterBaseScale = Vector3.one;
    private Coroutine objectivePunchRoutine;
    private Coroutine counterPunchRoutine;

    private void Start()
    {
        ResolvePlayerReferences();
        StyleHudText();

        // The HUD prefab may be dropped into a scene that has no GameManager yet.
        if (GameManager.Instance == null)
        {
            Debug.LogWarning("[HUDManager] GameManager not found. Component counter and objective will not update.");
            return;
        }

        GameManager.Instance.OnComponentCollected += UpdateComponentCounter;
        GameManager.Instance.OnObjectiveChanged += UpdateObjective;

        UpdateComponentCounter();
        UpdateObjective();
    }

    /// <summary>
    /// Replaces the flat white HUD labels with EXORA's palette (see
    /// ExoraPalette) and a slow glow pulse on the objective text, so it reads
    /// as a "live" HUD element instead of static text.
    /// </summary>
    private void StyleHudText()
    {
        if (objectiveText != null)
        {
            objectiveText.fontStyle = FontStyles.Bold;
            objectiveBaseScale = objectiveText.transform.localScale;
            StartCoroutine(UIAnimator.PulseColor(objectiveText, ExoraPalette.Accent, Color.white, 3f));
        }

        if (componentCounterText != null)
        {
            componentCounterText.fontStyle = FontStyles.Bold;
            componentCounterText.color = ExoraPalette.Accent;
            counterBaseScale = componentCounterText.transform.localScale;
        }
    }

    /// <summary>
    /// Resolves the player-side references from the scene's "Player"-tagged object so the
    /// Canvas prefab stays reusable across scenes with no manual Inspector wiring.
    /// </summary>
    private void ResolvePlayerReferences()
    {
        if (playerHealth != null && energySystem != null) return;

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (playerHealth == null)
        {
            if (player != null) playerHealth = player.GetComponent<PlayerHealth>();

            if (playerHealth != null)
                Debug.Log("[HUDManager] PlayerHealth found automatically");
            else
                Debug.LogWarning("[HUDManager] PlayerHealth not found. Health bar will not update.");
        }

        if (energySystem == null)
        {
            if (player != null) energySystem = player.GetComponent<EnergySystem>();

            if (energySystem != null)
                Debug.Log("[HUDManager] EnergySystem found automatically");
            else
                Debug.LogWarning("[HUDManager] EnergySystem not found. Energy bar will not update.");
        }
    }

    private void Update()
    {
        UpdateHealthBar();
        UpdateEnergyBar();
    }

    private void UpdateHealthBar()
    {
        if (playerHealth == null) return;

        int currentSegments = playerHealth.GetCurrentHealth();

        for (int i = 0; i < healthSegments.Length; i++)
        {
            healthSegments[i].color = i < currentSegments ? HEALTH_ACTIVE : HEALTH_INACTIVE;
        }
    }

    private void UpdateEnergyBar()
    {
        if (energySystem == null) return;

        float energyPercent = energySystem.GetCurrentEnergy() / energySystem.GetMaxEnergy();
        int activeSegments = Mathf.RoundToInt(energyPercent * 10);

        for (int i = 0; i < energySegments.Length; i++)
        {
            energySegments[i].color = i < activeSegments ? ENERGY_ACTIVE : ENERGY_INACTIVE;
        }
    }

    private void UpdateComponentCounter()
    {
        if (GameManager.Instance == null || componentCounterText == null) return;

        componentCounterText.text = $"{GameManager.Instance.GetComponentsCollected()} / {GameManager.Instance.GetTotalComponents()}";
        Punch(componentCounterText.transform, counterBaseScale, ref counterPunchRoutine);
    }

    private void UpdateObjective(string _ = null)
    {
        if (GameManager.Instance == null || objectiveText == null) return;

        objectiveText.text = GameManager.Instance.currentObjective;
        Punch(objectiveText.transform, objectiveBaseScale, ref objectivePunchRoutine);
    }

    /// <summary>Brief scale punch so a HUD value visibly "announces" a change instead of just swapping text.</summary>
    private void Punch(Transform target, Vector3 baseScale, ref Coroutine routine)
    {
        if (routine != null) StopCoroutine(routine);
        routine = StartCoroutine(PunchRoutine(target, baseScale));
    }

    private IEnumerator PunchRoutine(Transform target, Vector3 baseScale)
    {
        yield return UIAnimator.ScaleTo(target, baseScale * 1.3f, baseScale, 0.25f, true);
    }

    private void OnDestroy()
    {
        if (GameManager.Instance == null) return;
        GameManager.Instance.OnComponentCollected -= UpdateComponentCounter;
        GameManager.Instance.OnObjectiveChanged -= UpdateObjective;
    }
}
