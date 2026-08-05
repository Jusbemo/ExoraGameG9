using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Small, dependency-free tween helpers for UI polish. EXORA has no external
/// animation package installed (see Packages/manifest.json), so menu/HUD
/// "juice" runs through plain coroutines instead of DOTween or similar.
/// Pass useUnscaledTime = true for UI that must animate while paused
/// (Time.timeScale == 0), such as the pause menu.
/// </summary>
public static class UIAnimator
{
    public static IEnumerator FadeCanvasGroup(CanvasGroup group, float from, float to, float duration, bool useUnscaledTime = false)
    {
        if (group == null) yield break;

        group.alpha = from;
        float t = 0f;
        while (t < duration)
        {
            t += useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
            group.alpha = Mathf.Lerp(from, to, Mathf.Clamp01(t / duration));
            yield return null;
        }
        group.alpha = to;
    }

    public static IEnumerator ScaleTo(Transform target, Vector3 from, Vector3 to, float duration, bool useUnscaledTime = false)
    {
        if (target == null) yield break;

        float t = 0f;
        while (t < duration)
        {
            t += useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
            float e = EaseOutBack(Mathf.Clamp01(t / duration));
            target.localScale = Vector3.LerpUnclamped(from, to, e);
            yield return null;
        }
        target.localScale = to;
    }

    public static IEnumerator SlideTo(RectTransform target, Vector2 from, Vector2 to, float duration, bool useUnscaledTime = false)
    {
        if (target == null) yield break;

        target.anchoredPosition = from;
        float t = 0f;
        while (t < duration)
        {
            t += useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
            float e = EaseOutCubic(Mathf.Clamp01(t / duration));
            target.anchoredPosition = Vector2.LerpUnclamped(from, to, e);
            yield return null;
        }
        target.anchoredPosition = to;
    }

    /// <summary>Loops forever, breathing a Graphic's color between two values. Caller stops it via StopCoroutine.</summary>
    public static IEnumerator PulseColor(Graphic graphic, Color colorA, Color colorB, float periodSeconds)
    {
        while (graphic != null)
        {
            float t = (Mathf.Sin(Time.unscaledTime * (Mathf.PI * 2f / periodSeconds)) + 1f) * 0.5f;
            graphic.color = Color.Lerp(colorA, colorB, t);
            yield return null;
        }
    }

    private static float EaseOutBack(float x)
    {
        const float c1 = 1.70158f;
        const float c3 = c1 + 1f;
        float p = x - 1f;
        return 1f + c3 * p * p * p + c1 * p * p;
    }

    private static float EaseOutCubic(float x)
    {
        float p = 1f - x;
        return 1f - p * p * p;
    }
}
