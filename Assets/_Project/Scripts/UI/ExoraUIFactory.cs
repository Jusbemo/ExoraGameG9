using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Runtime factory for EXORA's menu/HUD UI. Building menus purely in code
/// keeps them a small, reviewable diff in Git (no giant hand-authored Canvas
/// prefabs) while matching the game's dark-blue / purple / cyan-green look
/// defined in ExoraPalette.
/// </summary>
public static class ExoraUIFactory
{
    public static GameObject CreateFullScreenCanvas(string name, int sortingOrder, out Canvas canvas, out CanvasGroup group)
    {
        GameObject go = new GameObject(name, typeof(RectTransform));
        canvas = go.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = sortingOrder;

        CanvasScaler scaler = go.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);
        scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
        scaler.matchWidthOrHeight = 0.5f;

        go.AddComponent<GraphicRaycaster>();
        group = go.AddComponent<CanvasGroup>();
        return go;
    }

    public static Image CreatePanel(Transform parent, string name, Color color, Vector2 anchorMin, Vector2 anchorMax, Vector2 offsetMin, Vector2 offsetMax)
    {
        GameObject go = new GameObject(name, typeof(RectTransform));
        go.transform.SetParent(parent, false);
        RectTransform rt = (RectTransform)go.transform;
        rt.anchorMin = anchorMin;
        rt.anchorMax = anchorMax;
        rt.offsetMin = offsetMin;
        rt.offsetMax = offsetMax;

        Image img = go.AddComponent<Image>();
        img.color = color;
        return img;
    }

    public static TMP_Text CreateText(Transform parent, string name, string text, float fontSize, Color color,
        TextAlignmentOptions align, Vector2 anchorMin, Vector2 anchorMax, Vector2 anchoredPos, Vector2 sizeDelta,
        FontStyles style = FontStyles.Normal)
    {
        GameObject go = new GameObject(name, typeof(RectTransform));
        go.transform.SetParent(parent, false);
        RectTransform rt = (RectTransform)go.transform;
        rt.anchorMin = anchorMin;
        rt.anchorMax = anchorMax;
        rt.pivot = new Vector2(0.5f, 0.5f);
        rt.anchoredPosition = anchoredPos;
        rt.sizeDelta = sizeDelta;

        TextMeshProUGUI tmp = go.AddComponent<TextMeshProUGUI>();
        tmp.text = text;
        tmp.fontSize = fontSize;
        tmp.color = color;
        tmp.alignment = align;
        tmp.fontStyle = style;
        tmp.enableWordWrapping = false;
        return tmp;
    }

    /// <summary>
    /// Stylized menu button: dark teal panel, purple outline, accent-colored
    /// bold label, plus hover/press "juice" via ExoraButtonHoverFx.
    /// </summary>
    public static Button CreateMenuButton(Transform parent, string name, string label, Vector2 anchoredPos, Vector2 size, System.Action onClick)
    {
        GameObject go = new GameObject(name, typeof(RectTransform));
        go.transform.SetParent(parent, false);
        RectTransform rt = (RectTransform)go.transform;
        rt.anchorMin = new Vector2(0.5f, 0.5f);
        rt.anchorMax = new Vector2(0.5f, 0.5f);
        rt.pivot = new Vector2(0.5f, 0.5f);
        rt.sizeDelta = size;
        rt.anchoredPosition = anchoredPos;

        Image bg = go.AddComponent<Image>();
        bg.color = ExoraPalette.DarkTeal;

        Outline outline = go.AddComponent<Outline>();
        outline.effectColor = ExoraPalette.Purple;
        outline.effectDistance = new Vector2(2f, -2f);

        Button button = go.AddComponent<Button>();
        ColorBlock colors = button.colors;
        colors.normalColor = Color.white;
        colors.highlightedColor = new Color(1.2f, 1.2f, 1.2f, 1f);
        colors.pressedColor = new Color(0.7f, 0.7f, 0.7f, 1f);
        colors.selectedColor = Color.white;
        colors.fadeDuration = 0.1f;
        button.colors = colors;
        button.targetGraphic = bg;

        CreateText(go.transform, "Label", label, 30, ExoraPalette.Accent, TextAlignmentOptions.Center,
            Vector2.zero, Vector2.one, Vector2.zero, Vector2.zero, FontStyles.Bold);

        if (onClick != null) button.onClick.AddListener(() => onClick());

        go.AddComponent<ExoraButtonHoverFx>();

        return button;
    }
}
