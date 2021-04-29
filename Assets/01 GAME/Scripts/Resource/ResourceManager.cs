using System;
using System.Collections.Generic;
using BD.Resource.SO;
using UnityEngine;

namespace BD.Resource
{
    public class ResourceManager : MonoBehaviour
    {
        public static ResourceManager Instance { get; private set; }

        public event EventHandler OnResourceAmountChanged;

        [SerializeField] List<ResourceAmount> startingResourceAmountList;
        Dictionary<ResourceTypeSO, int> _resourceAmountDictionary;


        void Awake()
        {
            Instance = this;
            _resourceAmountDictionary = new Dictionary<ResourceTypeSO, int>();

            ResourceTypeListSO resourceTypeList = Resources.Load<ResourceTypeListSO>(nameof(ResourceTypeListSO));

            foreach (ResourceTypeSO resourceType in resourceTypeList.list)
            {
                _resourceAmountDictionary[resourceType] = 0;
            }

            foreach (ResourceAmount resourceAmount in startingResourceAmountList)
            {
                AddResource(resourceAmount.resourceType, resourceAmount.amount);
            }
        }

        void TestLogResourceAmountDictionary()
        {
            foreach (ResourceTypeSO resourceType in _resourceAmountDictionary.Keys)
            {
                Debug.Log(resourceType.nameString + ": " + _resourceAmountDictionary[resourceType]);
            }
        }

        public void AddResource(ResourceTypeSO resourceType, int amount)
        {
            _resourceAmountDictionary[resourceType] += amount;
            OnResourceAmountChanged?.Invoke(this, EventArgs.Empty);
        }

        public int GetResourceAmount(ResourceTypeSO resourceType)
        {
            return _resourceAmountDictionary[resourceType];
        }

        public bool CanAfford(ResourceAmount[] resourceAmountArray)
        {
            foreach (ResourceAmount resourceAmount in resourceAmountArray)
            {
                if (GetResourceAmount(resourceAmount.resourceType) >= resourceAmount.amount)
                {
                    // Can afford
                }
                else
                {
                    // Cannot afford this!
                    return false;
                }
            }

            // Can afford all
            return true;
        }

        public void SpendResources(ResourceAmount[] resourceAmountArray)
        {
            foreach (ResourceAmount resourceAmount in resourceAmountArray)
            {
                _resourceAmountDictionary[resourceAmount.resourceType] -= resourceAmount.amount;
            }
        }
    }
}