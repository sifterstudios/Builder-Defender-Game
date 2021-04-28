using UnityEngine;
using UnityEngine.UI;

public class BuildingRepairButton : MonoBehaviour
{
    [SerializeField] HealthSystem healthSystem;
    [SerializeField] private ResourceTypeSO goldResourceType;


    void Awake()
    {
        transform.Find("button").GetComponent<Button>().onClick.AddListener(() =>
        {
            int missingHealth = healthSystem.GetHealthAmountMax() - healthSystem.GetHealthAmount();
            int repairCost = missingHealth / 2;

            ResourceAmount[] resourceAmountCost = new ResourceAmount[]
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