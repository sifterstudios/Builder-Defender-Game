using System;
using UnityEngine;

public class Building : MonoBehaviour
{
    HealthSystem _healthSystem;
    BuildingTypeSO _buildingType;
    Transform _buldingDemolishBtn;

    void Awake()
    {
        _buldingDemolishBtn = transform.Find("pfBuildingDemolishBtn");
        HideBuildingDemolishBtn();
    }

    void Start()
    {
        _buildingType = GetComponent<BuildingTypeHolder>().buildingType;
        _healthSystem = GetComponent<HealthSystem>();
        _healthSystem.SetHealthAmountMax(_buildingType.healthAmountMax, true);
        _healthSystem.OnDied += HealthSystemOnOnDied;
    }


    void HealthSystemOnOnDied(object sender, EventArgs e)
    {
        Destroy(gameObject);
    }

    void OnMouseEnter() => ShowBuildingDemolishBtn();


    void OnMouseExit() => HideBuildingDemolishBtn();


    void ShowBuildingDemolishBtn()
    {
        if (_buldingDemolishBtn != null)
        {
            _buldingDemolishBtn.gameObject.SetActive(true);
        }
    }

    void HideBuildingDemolishBtn()
    {
        if (_buldingDemolishBtn != null)
        {
            _buldingDemolishBtn.gameObject.SetActive(false);
        }
    }
}