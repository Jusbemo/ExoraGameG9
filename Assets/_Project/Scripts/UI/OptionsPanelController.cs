using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Shared "Opciones" overlay (currently: master volume). Reused by both
/// MainMenuController and PauseMenuController so there is a single place to
/// extend later (controls remap, etc). Built entirely at runtime via
/// ExoraUIFactory and starts hidden.
/// </summary>
public class OptionsPanelController : MonoBehaviour
{
    private CanvasGroup group;
    private Coroutine fadeRoutine;

    public static OptionsPanelController Build(Transform parent)
    {
        GameObject root = new GameObject("OptionsPanel", typeof(RectTransform));
        root.transform.SetParent(parent, false);
        RectTransform rootRt = (RectTransform)root.transform;
        rootRt.anchorMin = Vector2.zero;
        rootRt.anchorMax = Vector2.one;
        rootRt.offsetMin = Vector2.zero;
        rootRt.offsetMax = Vector2.zero;

        CanvasGroup cg = root.AddComponent<CanvasGroup>();
        cg.alpha = 0f;
        cg.blocksRaycasts = false;
        cg.interactable = false;

        ExoraUIFactory.CreatePanel(root.transform, "Dim", new Color(0.01f, 0.05f, 0.07f, 0.85f), Vector2.zero, Vector2.one, Vector2.zero, Vector2.zero);

        Image card = ExoraUIFactory.CreatePanel(root.transform, "Card", ExoraPalette.DarkTeal,
            new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), Vector2.zero, Vector2.zero);
        RectTransform cardRt = (RectTransform)card.transform;
        cardRt.pivot = new Vector2(0.5f, 0.5f);
        cardRt.sizeDelta = new Vector2(580, 380);
        cardRt.anchoredPosition = Vector2.zero;

        Outline cardOutline = card.gameObject.AddComponent<Outline>();
        cardOutline.effectColor = ExoraPalette.Purple;
        cardOutline.effectDistance = new Vector2(3f, -3f);

        ExoraUIFactory.CreateText(cardRt, "Header", "OPCIONES", 40, ExoraPalette.Accent, TextAlignmentOptions.Center,
            new Vector2(0.5f, 1f), new Vector2(0.5f, 1f), new Vector2(0, -60), new Vector2(500, 80), FontStyles.Bold);

        ExoraUIFactory.CreateText(cardRt, "VolLabel", "Volumen", 26, ExoraPalette.TextWhite, TextAlignmentOptions.Left,
            new Vector2(0.5f, 0.55f), new Vector2(0.5f, 0.55f), new Vector2(-210, 0), new Vector2(200, 40));

        GameObject sliderGO = new GameObject("VolumeSlider", typeof(RectTransform));
        sliderGO.transform.SetParent(cardRt, false);
        RectTransform sliderRt = (RectTransform)sliderGO.transform;
        sliderRt.anchorMin = new Vector2(0.5f, 0.55f);
        sliderRt.anchorMax = new Vector2(0.5f, 0.55f);
        sliderRt.pivot = new Vector2(0.5f, 0.5f);
        sliderRt.anchoredPosition = new Vector2(60, 0);
        sliderRt.sizeDelta = new Vector2(280, 24);

        Image sliderBg = sliderGO.AddComponent<Image>();
        sliderBg.color = ExoraPalette.DarkBlue;

        GameObject fillArea = new GameObject("FillArea", typeof(RectTransform));
        fillArea.transform.SetParent(sliderGO.transform, false);
        RectTransform fillAreaRt = (RectTransform)fillArea.transform;
        fillAreaRt.anchorMin = new Vector2(0f, 0.2f);
        fillAreaRt.anchorMax = new Vector2(1f, 0.8f);
        fillAreaRt.offsetMin = new Vector2(4, 0);
        fillAreaRt.offsetMax = new Vector2(-4, 0);

        GameObject fill = new GameObject("Fill", typeof(RectTransform));
        fill.transform.SetParent(fillArea.transform, false);
        Image fillImg = fill.AddComponent<Image>();
        fillImg.color = ExoraPalette.Accent;
        RectTransform fillRt = (RectTransform)fill.transform;
        fillRt.anchorMin = Vector2.zero;
        fillRt.anchorMax = new Vector2(1f, 1f);
        fillRt.offsetMin = Vector2.zero;
        fillRt.offsetMax = Vector2.zero;

        Slider slider = sliderGO.AddComponent<Slider>();
        slider.fillRect = fillRt;
        slider.targetGraphic = fillImg;
        slider.minValue = 0f;
        slider.maxValue = 1f;
        slider.value = AudioListener.volume;
        slider.onValueChanged.AddListener(v => AudioListener.volume = v);

        OptionsPanelController controller = root.AddComponent<OptionsPanelController>();
        controller.group = cg;

        ExoraUIFactory.CreateMenuButton(cardRt, "BtnClose", "CERRAR", new Vector2(0, -150), new Vector2(220, 56), controller.Close);

        return controller;
    }

    public void Open()
    {
        if (fadeRoutine != null) StopCoroutine(fadeRoutine);
        group.blocksRaycasts = true;
        group.interactable = true;
        fadeRoutine = StartCoroutine(UIAnimator.FadeCanvasGroup(group, group.alpha, 1f, 0.18f, true));
    }

    public void Close()
    {
        if (fadeRoutine != null) StopCoroutine(fadeRoutine);
        fadeRoutine = StartCoroutine(CloseRoutine());
    }

    private IEnumerator CloseRoutine()
    {
        yield return UIAnimator.FadeCanvasGroup(group, group.alpha, 0f, 0.18f, true);
        group.blocksRaycasts = false;
        group.interactable = false;
    }
}
