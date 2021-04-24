using System;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    float _timer;
    ResourceGeneratorData _resourceGeneratorData;
    float _timerMax;

    void Awake()
    {
        _resourceGeneratorData = GetComponent<BuildingTypeHolder>().buildingType.resourceGeneratorData;
        _timerMax = _resourceGeneratorData.timerMax;
    }

    void Start()
    {
        Collider2D[] collider2DArray =
            Physics2D.OverlapCircleAll(transform.position, _resourceGeneratorData.resourceDetectionRadius);

        int nearbyResourceAmount = 0;

        foreach (Collider2D collider2D in collider2DArray)
        {
            ResourceNode resourceNode = collider2D.GetComponent<ResourceNode>();
            if (resourceNode != null)
            {
                // It's a resource node!
                if (resourceNode.resourceType == _resourceGeneratorData.resourceType)
                {
                    // Same type!
                    nearbyResourceAmount++;
                }
            }
        }

        nearbyResourceAmount = Mathf.Clamp(nearbyResourceAmount, 0, _resourceGeneratorData.maxResourceAmount);

        if (nearbyResourceAmount == 0)
        {
            // NO resource nodes nearby
            // Disable resource generator
            enabled = false;
        }
        else
        {
            _timerMax = (_resourceGeneratorData.timerMax / 2f) + _resourceGeneratorData.timerMax *
                (1 - (float) nearbyResourceAmount / _resourceGeneratorData.maxResourceAmount);
        }

        Debug.Log("nearbyResourceAmount: " + nearbyResourceAmount + "; TimerMAX: " + _timerMax);
    }

    void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0f)
        {
            _timer += _timerMax;
            ResourceManager.Instance.AddResource(_resourceGeneratorData.resourceType, 1);
        }
    }
}