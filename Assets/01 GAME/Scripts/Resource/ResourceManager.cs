using System;
using System.Collections.Generic;
using Resource.SO;
using UnityEngine;

namespace Resource
{
    public class ResourceManager : MonoBehaviour
    {
        [SerializeField] List<ResourceAmount> startingResourceAmountList;
        Dictionary<ResourceTypeSO, int> _resourceAmountDictionary;
        public static ResourceManager Instance { get; private set; }


        void Awake()
        {
            Instance = this;
            _resourceAmountDictionary = new Dictionary<ResourceTypeSO, int>();

            var resourceTypeList = Resources.Load<ResourceTypeListSO>(nameof(ResourceTypeListSO));

            foreach (var resourceType in resourceTypeList.list) _resourceAmountDictionary[resourceType] = 0;

            foreach (var resourceAmount in startingResourceAmountList)
                AddResource(resourceAmount.resourceType, resourceAmount.amount);
        }

        public event EventHandler OnResourceAmountChanged;

        void TestLogResourceAmountDictionary()
        {
            foreach (var resourceType in _resourceAmountDictionary.Keys)
                Debug.Log(resourceType.nameString + ": " + _resourceAmountDictionary[resourceType]);
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
            foreach (var resourceAmount in resourceAmountArray)
                if (GetResourceAmount(resourceAmount.resourceType) >= resourceAmount.amount)
                {
                    // Can afford
                }
                else
                {
                    // Cannot afford this!
                    return false;
                }

            // Can afford all
            return true;
        }

        public void SpendResources(ResourceAmount[] resourceAmountArray)
        {
            foreach (var resourceAmount in resourceAmountArray)
                _resourceAmountDictionary[resourceAmount.resourceType] -= resourceAmount.amount;
        }
    }
}