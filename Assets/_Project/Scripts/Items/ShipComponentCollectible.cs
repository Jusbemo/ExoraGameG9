using UnityEngine;

namespace ExoraGame.Items
{
    public class ShipComponentCollectible : ItemCollectible
    {
        protected override void Awake()
        {
            base.Awake();

            // The ship component is the primary objective pickup, so it stands out
            // with a faster spin and a more dramatic collect pop than other pickups.
            collectScaleMultiplier = 2.0f;
            idleRotationSpeed = 90f;
        }

        protected override void OnCollect(GameObject player)
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddComponent();
                Debug.Log($"[Pickup] Ship component collected! Total: {GameManager.Instance.GetComponentsCollected()}/{GameManager.Instance.GetTotalComponents()}");
            }
            else
            {
                Debug.LogWarning("[Pickup] GameManager instance not found");
            }
        }
    }
}
