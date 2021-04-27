using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour
{
    public class OnActiveBuildingTypeChangedEventArgs : EventArgs
    {
        public BuildingTypeSO ActiveBuildingType;
    }

    public static BuildingManager Instance { get; private set; }
    public event EventHandler<OnActiveBuildingTypeChangedEventArgs> OnActiveBuildingTypeChanged;

    [SerializeField] Building hqBuilding;

    BuildingTypeListSO _buildingTypeList;
    BuildingTypeSO _activeBuildingType;
    Camera _mainCamera;


    private void Awake()
    {
        Instance = this;
        _buildingTypeList = Resources.Load<BuildingTypeListSO>(nameof(BuildingTypeListSO));
    }

    void Start()
    {
        _mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (_activeBuildingType != null)
            {
                if
                    (CanSpawnBuilding(_activeBuildingType, UtilsClass.GetMouseWorldPosition(), out string errorMessage))
                {
                    if (ResourceManager.Instance.CanAfford(_activeBuildingType.constructionResourceCostArray))
                    {
                        ResourceManager.Instance.SpendResources(_activeBuildingType.constructionResourceCostArray);
                        // Instantiate(_activeBuildingType.prefab, UtilsClass.GetMouseWorldPosition(),
                        //     Quaternion.identity);
                        BuildingConstruction.Create(UtilsClass.GetMouseWorldPosition(), _activeBuildingType);

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
        BoxCollider2D boxCollider2D = buildingType.prefab.GetComponent<BoxCollider2D>();

        Collider2D[] collider2DArray =
            Physics2D.OverlapBoxAll(position + (Vector3) boxCollider2D.offset, boxCollider2D.size, 0);
        bool isAreaClear = collider2DArray.Length == 0;
        if (!isAreaClear)
        {
            errorMessage = "Area is not clear!";
            return false;
        }

        collider2DArray = Physics2D.OverlapCircleAll(position, buildingType.minConstructionRadius);
        foreach (Collider2D collider2D in collider2DArray)
        {
            // Colliders inside the construction radius
            BuildingTypeHolder buildingTypeHolder = collider2D.GetComponent<BuildingTypeHolder>();
            if (buildingTypeHolder != null)
            {
                if (buildingTypeHolder.buildingType == buildingType)
                {
                    //There's already a building of this type within the construction radius!
                    errorMessage = "Too close ot another building of the same type!";
                    return false;
                }
            }
        }

        float maxConstructionRadius = 25f;
        collider2DArray = Physics2D.OverlapCircleAll(position, maxConstructionRadius);


        foreach (Collider2D collider2D in collider2DArray)
        {
            // Colliders inside the construction radius
            BuildingTypeHolder buildingTypeHolder = collider2D.GetComponent<BuildingTypeHolder>();
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
}