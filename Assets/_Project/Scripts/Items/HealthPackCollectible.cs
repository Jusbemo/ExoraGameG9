using UnityEngine;

namespace ExoraGame.Items
{
    public class HealthPackCollectible : ItemCollectible
    {
        [SerializeField] private int healAmount = 2;

        protected override void OnCollect(GameObject player)
        {
            PlayerHealth health = player.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.Heal(healAmount);
                Debug.Log($"[Pickup] Health pack collected: +{healAmount} HP");
            }
            else
            {
                Debug.LogWarning("[Pickup] Player has no PlayerHealth component");
            }
        }
    }
}
