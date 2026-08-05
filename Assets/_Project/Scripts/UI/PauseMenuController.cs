using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>
/// In-game pause overlay (REANUDAR / OPCIONES / SALIR AL MENU). Self-spawns
/// once at game start via RuntimeInitializeOnLoadMethod - the same
/// "persist across scenes" trick GameManager uses - so no existing level
/// scene file needs to be edited to wire this in, and it works whether the
/// game is started from 00_MainMenu or by pressing Play directly inside a
/// level scene while testing. Escape toggles it and freezes Time.timeScale
/// while open.
/// </summary>
public class PauseMenuController : MonoBehaviour
{
    private const string MainMenuSceneName = "00_MainMenu";

    private static PauseMenuController instance;

    private CanvasGroup group;
    private OptionsPanelController optionsPanel;
    private Coroutine fadeRoutine;
    private bool isPaused;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void Bootstrap()
    {
        if (instance != null) return;

        GameObject go = new GameObject("_UIRoot_PauseMenu");
        go.AddComponent<PauseMenuController>();
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        BuildUI();
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == MainMenuSceneName) return;
        if (!Input.GetKeyDown(KeyCode.Escape)) return;

        if (isPaused) Resume();
        else Pause();
    }

    private void BuildUI()
    {
        GameObject canvasGO = ExoraUIFactory.CreateFullScreenCanvas("PauseCanvas", 200, out _, out group);
        group.alpha = 0f;
        group.blocksRaycasts = false;
        group.interactable = false;

        ExoraUIFactory.CreatePanel(canvasGO.transform, "Dim", new Color(0.01f, 0.05f, 0.07f, 0.82f), Vector2.zero, Vector2.one, Vector2.zero, Vector2.zero);

        ExoraUIFactory.CreateText(canvasGO.transform, "Header", "PAUSA", 60, ExoraPalette.Accent, TextAlignmentOptions.Center,
            new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0, 190), new Vector2(700, 110), FontStyles.Bold);

        ExoraUIFactory.CreateMenuButton(canvasGO.transform, "BtnResume", "REANUDAR", new Vector2(0, 50), new Vector2(340, 70), Resume);
        ExoraUIFactory.CreateMenuButton(canvasGO.transform, "BtnOptions", "OPCIONES", new Vector2(0, -40), new Vector2(340, 70), () => optionsPanel.Open());
        ExoraUIFactory.CreateMenuButton(canvasGO.transform, "BtnExit", "SALIR AL MENU", new Vector2(0, -130), new Vector2(340, 70), ExitToMenu);

        optionsPanel = OptionsPanelController.Build(canvasGO.transform);
    }

    private void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f;
        group.blocksRaycasts = true;
        group.interactable = true;
        if (fadeRoutine != null) StopCoroutine(fadeRoutine);
        fadeRoutine = StartCoroutine(UIAnimator.FadeCanvasGroup(group, group.alpha, 1f, 0.15f, true));
    }

    private void Resume()
    {
        isPaused = false;
        Time.timeScale = 1f;
        group.blocksRaycasts = false;
        group.interactable = false;
        if (fadeRoutine != null) StopCoroutine(fadeRoutine);
        fadeRoutine = StartCoroutine(UIAnimator.FadeCanvasGroup(group, group.alpha, 0f, 0.15f, true));
    }

    private void ExitToMenu()
    {
        Time.timeScale = 1f;
        isPaused = false;
        SceneManager.LoadScene(MainMenuSceneName);
    }
}
