using System.Collections.Generic;
using UnityEngine;

namespace BD.Resource.SO
{
    [CreateAssetMenu(menuName = "ScriptableObjects/ResourceTypeList")]
    public class ResourceTypeListSO : ScriptableObject
    {
        public List<ResourceTypeSO> list;
    }
}