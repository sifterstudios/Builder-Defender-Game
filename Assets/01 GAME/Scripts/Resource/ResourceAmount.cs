using System;
using Resource.SO;

namespace Resource
{
    [Serializable]
    public class ResourceAmount
    {
        public ResourceTypeSO resourceType;
        public int amount;
    }
}