using BD.Resource;
using Resource;
using UnityEngine;
using Utilities;

namespace Building
{
    public class BuildingGhost : MonoBehaviour
    {
        ResourceNearbyOverlay _resourceNearbyOverlay;
        GameObject _spriteGameObject;

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

        void Update()
        {
            transform.position = UtilsClass.GetMouseWorldPosition();
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
                    _resourceNearbyOverlay.Show(e.ActiveBuildingType.resourceGeneratorData);
                else _resourceNearbyOverlay.Hide();
            }
        }

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