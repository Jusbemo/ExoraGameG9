using UnityEngine;

namespace ExoraGame.Items
{
    public class EnergyCellCollectible : ItemCollectible
    {
        [SerializeField] private float energyAmount = 20f;

        protected override void OnCollect(GameObject player)
        {
            EnergySystem energy = player.GetComponent<EnergySystem>();
            if (energy != null)
            {
                energy.AddEnergy(energyAmount);
                Debug.Log($"[Pickup] Energy cell collected: +{energyAmount} energy");
            }
            else
            {
                Debug.LogWarning("[Pickup] Player has no EnergySystem component");
            }
        }
    }
}
