using System;
using BD.Sound;
using Building.SO;
using Camera;
using Health;
using Resource;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;
using Utilities;

namespace Building
{
    public class BuildingManager : MonoBehaviour
    {
        [SerializeField] Building hqBuilding;

        //BuildingTypeListSO _buildingTypeList;
        BuildingTypeSO _activeBuildingType;

        public static BuildingManager Instance { get; private set; }
        //UnityEngine.Camera _mainCamera;


        void Awake()
        {
            Instance = this;
            // _buildingTypeList = Resources.Load<BuildingTypeListSO>(nameof(BuildingTypeListSO));
        }

        void Start()
        {
            // _mainCamera = UnityEngine.Camera.main;

            hqBuilding.GetComponent<HealthSystem>().OnDied += HQOnDied;
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
                if (_activeBuildingType != null)
                {
                    if
                        (CanSpawnBuilding(_activeBuildingType, UtilsClass.GetMouseWorldPosition(),
                            out var errorMessage))
                    {
                        if (ResourceManager.Instance.CanAfford(_activeBuildingType.constructionResourceCostArray))
                        {
                            ResourceManager.Instance.SpendResources(_activeBuildingType.constructionResourceCostArray);
                            BuildingConstruction.Create(UtilsClass.GetMouseWorldPosition(), _activeBuildingType);
                            SoundManager.Instance.PlaySound(SoundManager.Sound.BuildingPlaced);
                        }
                        else
                        {
                            TooltipUI.Instance.Show("Cannot afford " +
                                                    _activeBuildingType.GetConstructionResourceCostString(),
                                new TooltipUI.TooltipTimer {Timer = 2f});
                        }
                    }
                    else
                    {
                        TooltipUI.Instance.Show(errorMessage, new TooltipUI.TooltipTimer {Timer = 2f});
                    }
                }
        }

        public event EventHandler<OnActiveBuildingTypeChangedEventArgs> OnActiveBuildingTypeChanged;

        void HQOnDied(object sender, EventArgs e)
        {
            SoundManager.Instance.PlaySound(SoundManager.Sound.GameOver);
            CinemachineShake.Instance.ShakeCamera(25f, .5f);
            GameOverUI.Instance.Show();
        }


        public void SetActiveBuildingType(BuildingTypeSO buildingType)
        {
            _activeBuildingType = buildingType;
            OnActiveBuildingTypeChanged?.Invoke(this,
                new OnActiveBuildingTypeChangedEventArgs {ActiveBuildingType = _activeBuildingType});
        }

        public BuildingTypeSO GetActiveBuildingType()
        {
            return _activeBuildingType;
        }

        bool CanSpawnBuilding(BuildingTypeSO buildingType, Vector3 position, out string errorMessage)
        {
            var boxCollider2D = buildingType.prefab.GetComponent<BoxCollider2D>();

            var collider2DArray =
                Physics2D.OverlapBoxAll(position + (Vector3) boxCollider2D.offset, boxCollider2D.size, 0);
            var isAreaClear = collider2DArray.Length == 0;
            if (!isAreaClear)
            {
                errorMessage = "Area is not clear!";
                return false;
            }

            collider2DArray = Physics2D.OverlapCircleAll(position, buildingType.minConstructionRadius);
            foreach (var col in collider2DArray)
            {
                // Colliders inside the construction radius
                var buildingTypeHolder = col.GetComponent<BuildingTypeHolder>();
                if (buildingTypeHolder != null)
                    if (buildingTypeHolder.buildingType == buildingType)
                    {
                        //There's already a building of this type within the construction radius!
                        errorMessage = "Too close ot another building of the same type!";
                        return false;
                    }
            }

            if (buildingType.hasResourceGeneratorData)
            {
                ResourceGeneratorData resourceGeneratorData = buildingType.resourceGeneratorData;
                int nearbyResourceAmount =
                    ResourceGenerator.GetNearbyResourceAmount(resourceGeneratorData, position);


                if (nearbyResourceAmount == 0)
                {
                    errorMessage = "There are no nearby Resource Nodes!";
                    return false;
                }
            }


            var maxConstructionRadius = 25f;
            collider2DArray = Physics2D.OverlapCircleAll(position, maxConstructionRadius);


            foreach (var col in collider2DArray)
            {
                // Colliders inside the construction radius
                var buildingTypeHolder = col.GetComponent<BuildingTypeHolder>();
                if (buildingTypeHolder != null)
                {
                    // It's a building!
                    errorMessage = "";
                    return true;
                }
            }

            errorMessage = "Too far from any other building!";
            return false;
        }

        public Building GetHqBuilding()
        {
            return hqBuilding;
        }

        public class OnActiveBuildingTypeChangedEventArgs : EventArgs
        {
            public BuildingTypeSO ActiveBuildingType;
        }
    }
}