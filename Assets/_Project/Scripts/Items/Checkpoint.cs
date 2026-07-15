using System.Collections;
using UnityEngine;

namespace ExoraGame.Items
{
    // Not an ItemCollectible: a checkpoint never gets picked up or destroyed,
    // it persists in the world and just flips visual state once activated.
    public class Checkpoint : MonoBehaviour
    {
        [Header("Colors")]
        [SerializeField] private Color inactiveColor = new Color(1f, 1f, 1f, 1f);
        [SerializeField] private Color activeColor = new Color(0.388f, 0.874f, 0.306f, 1f);

        [Header("Idle Animation")]
        [SerializeField] private float idleBobAmplitude = 0.15f;
        [SerializeField] private float idleBobFrequency = 1.5f;

        [Header("Inactive Pulse")]
        [Tooltip("Alpha pulse while inactive draws the player's attention toward unused checkpoints.")]
        [SerializeField] private float pulseFrequency = 3f;
        [SerializeField] private float pulseMinAlpha = 0.7f;

        [Header("Activation Feedback")]
        [SerializeField] private float activationPopDuration = 0.15f;
        [SerializeField] private float activationPopScaleMultiplier = 1.3f;

        // Guards against re-saving the same position on every subsequent overlap
        // once a checkpoint has already been activated.
        private bool isActivated = false;

        private Vector3 initialPosition;
        private Vector3 initialScale;
        private SpriteRenderer sr;

        private void Awake()
        {
            initialPosition = transform.position;
            initialScale = transform.localScale;
            sr = GetComponent<SpriteRenderer>();
            if (sr == null) sr = GetComponentInChildren<SpriteRenderer>();

            Collider2D col = GetComponent<Collider2D>();
            if (col == null || !col.isTrigger)
            {
                Debug.LogWarning($"[Checkpoint] {gameObject.name} has no Collider2D configured as a trigger.", this);
            }

            if (sr != null) sr.color = inactiveColor;
        }

        private void Update()
        {
            float newY = initialPosition.y + Mathf.Sin(Time.time * idleBobFrequency * 2f * Mathf.PI) * idleBobAmplitude;
            transform.position = new Vector3(initialPosition.x, newY, initialPosition.z);

            if (sr == null) return;

            if (!isActivated)
            {
                float alpha = Mathf.Lerp(pulseMinAlpha, 1f, (Mathf.Sin(Time.time * pulseFrequency * 2f * Mathf.PI) + 1f) / 2f);
                sr.color = new Color(inactiveColor.r, inactiveColor.g, inactiveColor.b, alpha);
            }
            else
            {
                sr.color = activeColor;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (isActivated) return;
            if (!other.CompareTag("Player")) return;

            isActivated = true;
            if (sr != null) sr.color = activeColor;

            if (GameManager.Instance != null)
            {
                GameManager.Instance.SetCheckpoint(transform.position);
            }

            Debug.Log($"[Checkpoint] Activated at {transform.position}");
            StartCoroutine(ActivationFeedback());
        }

        private IEnumerator ActivationFeedback()
        {
            Vector3 poppedScale = initialScale * activationPopScaleMultiplier;
            float halfDuration = activationPopDuration / 2f;

            float elapsed = 0f;
            while (elapsed < halfDuration)
            {
                elapsed += Time.deltaTime;
                float t = halfDuration > 0f ? Mathf.Clamp01(elapsed / halfDuration) : 1f;
                transform.localScale = Vector3.Lerp(initialScale, poppedScale, t);
                yield return null;
            }

            elapsed = 0f;
            while (elapsed < halfDuration)
            {
                elapsed += Time.deltaTime;
                float t = halfDuration > 0f ? Mathf.Clamp01(elapsed / halfDuration) : 1f;
                transform.localScale = Vector3.Lerp(poppedScale, initialScale, t);
                yield return null;
            }

            transform.localScale = initialScale;
        }
    }
}
