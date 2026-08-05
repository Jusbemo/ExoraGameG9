using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>
/// Builds and animates EXORA's main menu entirely at runtime via
/// ExoraUIFactory, so the menu can't drift out of sync with ExoraPalette and
/// stays a tiny diff in Git instead of a hand-authored Canvas prefab.
/// Attach to an empty GameObject in Assets/_Project/Scenes/00_MainMenu.unity.
/// </summary>
public class MainMenuController : MonoBehaviour
{
    [Tooltip("Scene loaded when pressing JUGAR. There is no save/continue system yet, so this always starts a fresh run.")]
    [SerializeField] private string firstLevelScene = "01_PlanetLanding";

    private CanvasGroup rootGroup;
    private OptionsPanelController optionsPanel;

    private void Start()
    {
        Time.timeScale = 1f; // safety net in case we arrived here via "Salir al menu" while paused
        BuildUI();
    }

    private void BuildUI()
    {
        GameObject canvasGO = ExoraUIFactory.CreateFullScreenCanvas("MainMenuCanvas", 100, out _, out rootGroup);

        ExoraUIFactory.CreatePanel(canvasGO.transform, "BG", ExoraPalette.DarkBlue, Vector2.zero, Vector2.one, Vector2.zero, Vector2.zero);
        ExoraUIFactory.CreatePanel(canvasGO.transform, "BG_Accent", new Color(ExoraPalette.DarkTeal.r, ExoraPalette.DarkTeal.g, ExoraPalette.DarkTeal.b, 0.5f),
            new Vector2(0f, 0f), new Vector2(1f, 0.42f), Vector2.zero, Vector2.zero);

        TMP_Text title = ExoraUIFactory.CreateText(canvasGO.transform, "Title", "EXORA", 130, ExoraPalette.Accent,
            TextAlignmentOptions.Center, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0, 280), new Vector2(900, 180), FontStyles.Bold);

        ExoraUIFactory.CreateText(canvasGO.transform, "Subtitle", "un planeta hostil. tres componentes. una salida.", 24, ExoraPalette.Purple,
            TextAlignmentOptions.Center, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0, 190), new Vector2(900, 50));

        ExoraUIFactory.CreateMenuButton(canvasGO.transform, "BtnPlay", "JUGAR", new Vector2(0, 20), new Vector2(360, 76), OnPlayPressed);
        ExoraUIFactory.CreateMenuButton(canvasGO.transform, "BtnOptions", "OPCIONES", new Vector2(0, -80), new Vector2(360, 76), OnOptionsPressed);
        ExoraUIFactory.CreateMenuButton(canvasGO.transform, "BtnQuit", "SALIR", new Vector2(0, -180), new Vector2(360, 76), OnQuitPressed);

        ExoraUIFactory.CreateText(canvasGO.transform, "Credits", "Grupo 9 - Universidad Fidelitas", 18, new Color(1f, 1f, 1f, 0.35f),
            TextAlignmentOptions.Center, new Vector2(0.5f, 0f), new Vector2(0.5f, 0f), new Vector2(0, 30), new Vector2(700, 30));

        optionsPanel = OptionsPanelController.Build(canvasGO.transform);

        StartCoroutine(UIAnimator.PulseColor(title, ExoraPalette.Accent, Color.white, 2.6f));
        StartCoroutine(UIAnimator.FadeCanvasGroup(rootGroup, 0f, 1f, 0.6f, true));
    }

    private void OnPlayPressed() => StartCoroutine(TransitionAndLoad(firstLevelScene));
    private void OnOptionsPressed() => optionsPanel.Open();

    private void OnQuitPressed()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private IEnumerator TransitionAndLoad(string sceneName)
    {
        rootGroup.interactable = false;
        rootGroup.blocksRaycasts = false;
        yield return UIAnimator.FadeCanvasGroup(rootGroup, rootGroup.alpha, 0f, 0.35f, true);
        SceneManager.LoadScene(sceneName);
    }
}
