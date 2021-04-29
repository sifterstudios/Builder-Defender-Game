using System;
using BD.Building.SO;
using BD.Health;
using BD.Sound;
using UnityEngine;

namespace BD.Building
{
    public class Building : MonoBehaviour
    {
        HealthSystem _healthSystem;
        BuildingTypeSO _buildingType;
        Transform _buildingDemolishBtn;
        Transform _buildingRepairBtn;

        void Awake()
        {
            _buildingDemolishBtn = transform.Find("pfBuildingDemolishBtn");
            _buildingRepairBtn = transform.Find("pfBuildingRepairBtn");
            HideBuildingDemolishBtn();
            HideBuildingRepairBtn();
        }

        void Start()
        {
            _buildingType = GetComponent<BuildingTypeHolder>().buildingType;
            _healthSystem = GetComponent<HealthSystem>();
            _healthSystem.SetHealthAmountMax(_buildingType.healthAmountMax, true);
            _healthSystem.OnDamaged += HealthSystem_OnDamaged;
            _healthSystem.OnHealed += HealthSystem_OnHealed;
            _healthSystem.OnDied += HealthSystem_OnDied;
        }

        void HealthSystem_OnHealed(object sender, EventArgs e)
        {
            if (_healthSystem.IsFullHealth())
            {
                HideBuildingRepairBtn();
            }
        }

        void HealthSystem_OnDamaged(object sender, EventArgs e)
        {
            ShowBuildingRepairBtn();
            SoundManager.Instance.PlaySound(SoundManager.Sound.BuildingDamaged);
        }


        void HealthSystem_OnDied(object sender, EventArgs e)
        {
            Destroy(gameObject);
            SoundManager.Instance.PlaySound(SoundManager.Sound.BuildingDestroyed);
        }

        void OnMouseEnter() => ShowBuildingDemolishBtn();


        void OnMouseExit() => HideBuildingDemolishBtn();


        void ShowBuildingDemolishBtn()
        {
            if (_buildingDemolishBtn != null)
            {
                _buildingDemolishBtn.gameObject.SetActive(true);
            }
        }

        void HideBuildingDemolishBtn()
        {
            if (_buildingDemolishBtn != null)
            {
                _buildingDemolishBtn.gameObject.SetActive(false);
            }
        }

        void HideBuildingRepairBtn()
        {
            if (_buildingRepairBtn != null)
            {
                _buildingRepairBtn.gameObject.SetActive(false);
            }
        }
        void ShowBuildingRepairBtn()
        {
            if (_buildingRepairBtn != null)
            {
                _buildingRepairBtn.gameObject.SetActive(true);
            }
        }
    }
}