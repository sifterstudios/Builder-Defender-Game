using TMPro;
using UnityEngine;

namespace BD.Resource
{
    public class ResourceNearbyOverlay : MonoBehaviour
    {
        ResourceGeneratorData _resourceGeneratorData;

        void Awake()
        {
            Hide();
        }

        void Update()
        {
            int nearbyResourceAmount = ResourceGenerator.GetNearbyResourceAmount(_resourceGeneratorData, transform.position);
            float percent = Mathf.RoundToInt((float) nearbyResourceAmount / _resourceGeneratorData.maxResourceAmount * 100f);
            transform.Find("text").GetComponent<TextMeshPro>().SetText(percent + "%");
        }

        public void Show(ResourceGeneratorData resourceGeneratorData)
        {
            this._resourceGeneratorData = resourceGeneratorData;
            gameObject.SetActive(true);

            transform.Find("icon").GetComponent<SpriteRenderer>().sprite = resourceGeneratorData.resourceType.sprite;

        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}