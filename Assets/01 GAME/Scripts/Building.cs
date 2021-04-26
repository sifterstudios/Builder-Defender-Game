using System;
using UnityEngine;

public class Building : MonoBehaviour
{
    HealthSystem _healthSystem;
    BuildingTypeSO _buildingType;

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
}