using Health;
using Resource;
using Resource.SO;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Building
{
    public class BuildingRepairButton : MonoBehaviour
    {
        [SerializeField] HealthSystem healthSystem;
        [SerializeField] ResourceTypeSO goldResourceType;


        void Awake()
        {
            transform.Find("button").GetComponent<Button>().onClick.AddListener(() =>
            {
                var missingHealth = healthSystem.GetHealthAmountMax() - healthSystem.GetHealthAmount();
                var repairCost = missingHealth / 2;

                ResourceAmount[] resourceAmountCost =
                    {new ResourceAmount {resourceType = goldResourceType, amount = repairCost}};
                if (ResourceManager.Instance.CanAfford(resourceAmountCost))
                {
                    // Cam afford the repairs
                    ResourceManager.Instance.SpendResources(resourceAmountCost);
                    healthSystem.HealFull();
                }
                else
                {
                    // Cannot afford the repairs!
                    TooltipUI.Instance.Show("Cannot afford repair cost!", new TooltipUI.TooltipTimer {Timer = 2f});
                }
            });
        }
    }
}