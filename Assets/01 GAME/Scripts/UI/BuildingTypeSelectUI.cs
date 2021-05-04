using System.Collections.Generic;
using Building;
using Building.SO;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace UI
{
    public class BuildingTypeSelectUI : MonoBehaviour
    {
        [SerializeField] Sprite arrowSprite;
        [SerializeField] List<BuildingTypeSO> ignoreBuildingTypeList;
        Transform _arrowBtn;
        Dictionary<BuildingTypeSO, Transform> _btnTransformDictionary;

        void Awake()
        {
            var btnTemplate = transform.Find("btnTemplate");
            btnTemplate.gameObject.SetActive(false);
            _btnTransformDictionary = new Dictionary<BuildingTypeSO, Transform>();
            var buildingTypeList = Resources.Load<BuildingTypeListSO>(nameof(BuildingTypeListSO));

            var index = 0;
            _arrowBtn = Instantiate(btnTemplate, transform);
            _arrowBtn.gameObject.SetActive(true);

            var offsetAmount = 130f;
            _arrowBtn.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);

            _arrowBtn.Find("image").GetComponent<Image>().sprite = arrowSprite;
            _arrowBtn.Find("image").GetComponent<RectTransform>().sizeDelta = new Vector2(0, -30);


            _arrowBtn.GetComponent<Button>().onClick
                .AddListener(() => BuildingManager.Instance.SetActiveBuildingType(null));

            var mouseEnterExitEvents = _arrowBtn.GetComponent<MouseEnterExitEvents>();
            mouseEnterExitEvents.OnMouseEnter += (sender, e) => { TooltipUI.Instance.Show("Arrow"); };
            mouseEnterExitEvents.OnMouseExit += (sender, e) => { TooltipUI.Instance.Hide(); };


            index++;

            foreach (var buildingType in buildingTypeList.list)
            {
                if (ignoreBuildingTypeList.Contains(buildingType)) continue;
                var btnTransform = Instantiate(btnTemplate, transform);
                btnTransform.gameObject.SetActive(true);

                offsetAmount = 130f;
                btnTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);

                btnTransform.Find("image").GetComponent<Image>().sprite = buildingType.sprite;

                btnTransform.GetComponent<Button>().onClick
                    .AddListener(() => BuildingManager.Instance.SetActiveBuildingType(buildingType));

                mouseEnterExitEvents = btnTransform.GetComponent<MouseEnterExitEvents>();
                mouseEnterExitEvents.OnMouseEnter += (sender, e) =>
                {
                    TooltipUI.Instance.Show(buildingType.nameString + "\n" +
                                            buildingType.GetConstructionResourceCostString());
                };
                mouseEnterExitEvents.OnMouseExit += (sender, e) => { TooltipUI.Instance.Hide(); };
                _btnTransformDictionary[buildingType] = btnTransform;

                index++;
            }
        }

        void Start()
        {
            BuildingManager.Instance.OnActiveBuildingTypeChanged += BuildingManager_OnActiveBuildingTypeChanged;
            UpdateActiveBuildingTypeButton();
        }

        void BuildingManager_OnActiveBuildingTypeChanged(object sender,
            BuildingManager.OnActiveBuildingTypeChangedEventArgs e)
        {
            UpdateActiveBuildingTypeButton();
        }


        void UpdateActiveBuildingTypeButton()
        {
            _arrowBtn.Find("selected").gameObject.SetActive(false);
            foreach (var buildingType in _btnTransformDictionary.Keys)
            {
                var btnTransform = _btnTransformDictionary[buildingType];
                btnTransform.Find("selected").gameObject.SetActive(false);
            }

            var activeBuildingType = BuildingManager.Instance.GetActiveBuildingType();
            if (activeBuildingType == null)
                _arrowBtn.Find("selected").gameObject.SetActive(true);
            else
                _btnTransformDictionary[activeBuildingType].Find("selected").gameObject.SetActive(true);
        }
    }
}