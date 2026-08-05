using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Small hover/press "juice" for EXORA menu buttons: a quick scale punch on
/// hover, press and release. Added automatically by
/// ExoraUIFactory.CreateMenuButton, no manual wiring needed.
/// </summary>
[RequireComponent(typeof(RectTransform))]
public class ExoraButtonHoverFx : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    private RectTransform rt;
    private Vector3 baseScale;
    private Coroutine routine;

    private void Awake()
    {
        rt = (RectTransform)transform;
        baseScale = rt.localScale;
    }

    private void OnDisable()
    {
        if (rt != null) rt.localScale = baseScale;
    }

    public void OnPointerEnter(PointerEventData eventData) => AnimateTo(baseScale * 1.08f, 0.12f);
    public void OnPointerExit(PointerEventData eventData) => AnimateTo(baseScale, 0.12f);
    public void OnPointerDown(PointerEventData eventData) => AnimateTo(baseScale * 0.94f, 0.06f);
    public void OnPointerUp(PointerEventData eventData) => AnimateTo(baseScale * 1.08f, 0.08f);

    private void AnimateTo(Vector3 target, float duration)
    {
        if (!gameObject.activeInHierarchy) return;
        if (routine != null) StopCoroutine(routine);
        routine = StartCoroutine(UIAnimator.ScaleTo(rt, rt.localScale, target, duration, true));
    }
}
