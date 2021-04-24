using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour
{
    #region Variables

    public static BuildingManager Instance { get; private set; }
    BuildingTypeListSO _buildingTypeList;
    BuildingTypeSO _activeBuildingType;
    Camera _mainCamera;

    #endregion

    #region Unity Methods

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
                Instantiate(_activeBuildingType.prefab, GetMouseWorldPosition(), Quaternion.identity);
            }
        }
    }

    Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseWorldPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;
        return mouseWorldPosition;
    }

    public void SetActiveBuildingType(BuildingTypeSO buildingType)
    {
        _activeBuildingType = buildingType;
    }

    public BuildingTypeSO GetActiveBuildingType()
    {
        return _activeBuildingType;
    }

    #endregion
}