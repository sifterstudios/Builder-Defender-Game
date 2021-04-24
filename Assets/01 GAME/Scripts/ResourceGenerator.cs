using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    float _timer;
    BuildingTypeSO _buildingType;
    float _timerMax;

    void Awake()
    {
        _buildingType = GetComponent<BuildingTypeHolder>().buildingType;
        _timerMax = _buildingType.resourceGeneratorData.timerMax;
    }

    void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0f)
        {
            _timer += _timerMax;
            ResourceManager.Instance.AddResource(_buildingType.resourceGeneratorData.resourceType, 1);
        }
    }
}