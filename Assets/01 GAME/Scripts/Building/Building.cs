using System;
using BD.Sound;
using Building.SO;
using Camera;
using Health;
using UnityEngine;

namespace Building
{
    public class Building : MonoBehaviour
    {
        Transform _buildingDemolishBtn;
        Transform _buildingRepairBtn;
        BuildingTypeSO _buildingType;
        HealthSystem _healthSystem;

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

        void OnMouseEnter()
        {
            ShowBuildingDemolishBtn();
        }


        void OnMouseExit()
        {
            HideBuildingDemolishBtn();
        }

        void HealthSystem_OnHealed(object sender, EventArgs e)
        {
            if (_healthSystem.IsFullHealth()) HideBuildingRepairBtn();
        }

        void HealthSystem_OnDamaged(object sender, EventArgs e)
        {
            ShowBuildingRepairBtn();
            SoundManager.Instance.PlaySound(SoundManager.Sound.BuildingDamaged);
            CinemachineShake.Instance.ShakeCamera(7f, .15f);
            ChromaticAberrationEffect.Instance.SetWeight(1f);
        }


        void HealthSystem_OnDied(object sender, EventArgs e)
        {
            Instantiate(Resources.Load<Transform>("pfBuildingDestroyedParticles"),
                transform.position, Quaternion.identity);
            Destroy(gameObject);
            SoundManager.Instance.PlaySound(SoundManager.Sound.BuildingDestroyed);
            CinemachineShake.Instance.ShakeCamera(10f, .2f);
            ChromaticAberrationEffect.Instance.SetWeight(1f);
        }


        void ShowBuildingDemolishBtn()
        {
            if (_buildingDemolishBtn != null) _buildingDemolishBtn.gameObject.SetActive(true);
        }

        void HideBuildingDemolishBtn()
        {
            if (_buildingDemolishBtn != null) _buildingDemolishBtn.gameObject.SetActive(false);
        }

        void HideBuildingRepairBtn()
        {
            if (_buildingRepairBtn != null) _buildingRepairBtn.gameObject.SetActive(false);
        }

        void ShowBuildingRepairBtn()
        {
            if (_buildingRepairBtn != null) _buildingRepairBtn.gameObject.SetActive(true);
        }
    }
}