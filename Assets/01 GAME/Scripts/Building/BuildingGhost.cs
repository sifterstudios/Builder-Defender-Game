using BD.Resource;
using BD.Utilities;
using UnityEngine;

namespace BD.Building
{
    public class BuildingGhost : MonoBehaviour
    {
        GameObject _spriteGameObject;
        ResourceNearbyOverlay _resourceNearbyOverlay;

        void Awake()
        {
            _spriteGameObject = transform.Find("sprite").gameObject;
            _resourceNearbyOverlay = transform.Find("pfResourceNearbyOverlay").GetComponent<ResourceNearbyOverlay>();
            Hide();
        }

        void Start()
        {
            BuildingManager.Instance.OnActiveBuildingTypeChanged += BuildingManager_OnActiveBuildingTypeChanged;
        }

        void BuildingManager_OnActiveBuildingTypeChanged(object sender,
            BuildingManager.OnActiveBuildingTypeChangedEventArgs e)
        {
            if (e.ActiveBuildingType == null)
            {
                Hide();
                _resourceNearbyOverlay.Hide();
            }
            else
            {
                Show(e.ActiveBuildingType.sprite);
                if (e.ActiveBuildingType.hasResourceGeneratorData)
                {
                    _resourceNearbyOverlay.Show(e.ActiveBuildingType.resourceGeneratorData);
                }
                else _resourceNearbyOverlay.Hide();
            }
        }

        void Update() => transform.position = UtilsClass.GetMouseWorldPosition();

        void Show(Sprite ghostSprite)
        {
            _spriteGameObject.SetActive(true);
            _spriteGameObject.GetComponent<SpriteRenderer>().sprite = ghostSprite;
        }

        void Hide()
        {
            _spriteGameObject.SetActive(false);
        }
    }
}