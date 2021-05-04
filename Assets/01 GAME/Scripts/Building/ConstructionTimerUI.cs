using UnityEngine;
using UnityEngine.UI;

namespace Building
{
    public class ConstructionTimerUI : MonoBehaviour
    {
        [SerializeField] BuildingConstruction buildingConstruction;
        Image _constructionProgressImage;

        void Awake()
        {
            _constructionProgressImage = transform.Find("mask").Find("image").GetComponent<Image>();
        }

        void Update()
        {
            _constructionProgressImage.fillAmount = buildingConstruction.GetConstructionTimerNormalized();
        }
    }
}