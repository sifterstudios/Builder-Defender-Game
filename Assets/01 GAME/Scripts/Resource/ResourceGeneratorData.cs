using System;
using Resource.SO;

namespace Resource
{
    [Serializable]
    public class ResourceGeneratorData
    {
        public float timerMax;
        public ResourceTypeSO resourceType;
        public float resourceDetectionRadius;
        public int maxResourceAmount;
    }
}