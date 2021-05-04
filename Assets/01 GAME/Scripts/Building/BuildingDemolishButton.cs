using Resource;
using UnityEngine;
using UnityEngine.UI;

namespace Building
{
    public class BuildingDemolishButton : MonoBehaviour
    {
        [SerializeField] Building building;

        void Awake()
        {
            transform.Find("button").GetComponent<Button>().onClick.AddListener(() =>
            {
                var buildingType = building.GetComponent<BuildingTypeHolder>().buildingType;
                foreach (var resourceAmount in buildingType.constructionResourceCostArray)
                {
                    ResourceManager.Instance.AddResource(resourceAmount.resourceType,
                        Mathf.FloorToInt(resourceAmount.amount * .6f));
                    Destroy(building.gameObject);
                }
            });
        }
    }
}