using TMPro;
using UnityEngine;

namespace Resource
{
    public class ResourceGeneratorOverlay : MonoBehaviour
    {
        [SerializeField] ResourceGenerator _resourceGenerator;
        Transform _barTransform;

        void Start()
        {
            _barTransform = transform.Find("bar");
            var resourceGeneratorData = _resourceGenerator.GetResourceGeneratorData();
            transform.Find("icon").GetComponent<SpriteRenderer>().sprite = resourceGeneratorData.resourceType.sprite;
            transform.Find("text").GetComponent<TextMeshPro>()
                .SetText(_resourceGenerator.GetAmountGeneratedPerSecond().ToString("F1"));
        }

        void Update()
        {
            _barTransform.localScale = new Vector3(1 - _resourceGenerator.GetTimerNormalized(), 1, 1);
        }
    }
}