using System.Collections;
using UnityEngine;

namespace ExoraGame.Items
{
    /// <summary>
    /// Base class for all pickups: idle rotation/bob animation, trigger-based
    /// collection guarded against double-firing, and a shared scale+fade feedback.
    /// </summary>
    public abstract class ItemCollectible : MonoBehaviour
    {
        [Header("Idle Animation")]
        [SerializeField] protected float idleRotationSpeed = 45f;
        [SerializeField] protected float idleBobAmplitude = 0.1f;
        [SerializeField] protected float idleBobFrequency = 2f;

        [Header("Collect Feedback")]
        [SerializeField] protected float collectAnimDuration = 0.15f;
        [SerializeField] protected float collectScaleMultiplier = 1.5f;

        // Guards against OnTriggerEnter2D firing again during the same frame (e.g. multiple
        // player colliders) before the collect coroutine has disabled the trigger collider.
        private bool isCollected = false;

        // Cached once in Awake: Update() rewrites transform.position every frame for the bob,
        // so the original spawn position is the only stable reference point to bob around.
        private Vector3 initialPosition;

        // Cached so the collect feedback always lerps from each instance's actual placed
        // scale, not a hardcoded Vector3.one that would ignore per-prefab scale overrides.
        private Vector3 initialScale;

        private SpriteRenderer spriteRenderer;
        private Collider2D triggerCollider;

        // Rotates the child sprite if the visual lives on a separate child transform;
        // otherwise falls back to rotating this GameObject directly.
        private Transform visualTransform;

        protected virtual void Awake()
        {
            initialPosition = transform.position;
            initialScale = transform.localScale;

            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            triggerCollider = GetComponent<Collider2D>();
            visualTransform = (spriteRenderer != null && spriteRenderer.transform != transform)
                ? spriteRenderer.transform
                : transform;

            if (triggerCollider == null || !triggerCollider.isTrigger)
            {
                Debug.LogWarning($"[ItemCollectible] {gameObject.name} has no Collider2D configured as a trigger.", this);
            }
        }

        private void Update()
        {
            if (isCollected) return;

            visualTransform.Rotate(0f, 0f, idleRotationSpeed * Time.deltaTime);

            float newY = initialPosition.y + Mathf.Sin(Time.time * idleBobFrequency * 2f * Mathf.PI) * idleBobAmplitude;
            transform.position = new Vector3(initialPosition.x, newY, initialPosition.z);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (isCollected) return;
            if (!other.CompareTag("Player")) return;

            // Set before OnCollect/StartCoroutine so no code path below can re-enter this method.
            isCollected = true;
            OnCollect(other.gameObject);
            StartCoroutine(PlayCollectFeedback());
        }

        protected abstract void OnCollect(GameObject player);

        private IEnumerator PlayCollectFeedback()
        {
            // Disable immediately (not at coroutine end) so any collider still overlapping
            // this frame can't re-trigger collection while the feedback animation plays.
            if (triggerCollider != null) triggerCollider.enabled = false;

            float halfDuration = collectAnimDuration / 2f;
            Vector3 targetScale = initialScale * collectScaleMultiplier;
            Color color = spriteRenderer != null ? spriteRenderer.color : Color.white;

            float elapsed = 0f;
            while (elapsed < halfDuration)
            {
                elapsed += Time.deltaTime;
                float t = halfDuration > 0f ? Mathf.Clamp01(elapsed / halfDuration) : 1f;

                transform.localScale = Vector3.Lerp(initialScale, targetScale, t);
                if (spriteRenderer != null)
                {
                    color.a = Mathf.Lerp(1f, 0f, t);
                    spriteRenderer.color = color;
                }

                yield return null;
            }

            Destroy(gameObject);
        }
    }
}
