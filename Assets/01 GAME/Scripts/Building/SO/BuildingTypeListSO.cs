using System.Collections.Generic;
using UnityEngine;

namespace BD.Building.SO
{
    [CreateAssetMenu(menuName = "ScriptableObjects/BuildingTypeList")]
    public class BuildingTypeListSO : ScriptableObject
    {
        public List<BuildingTypeSO> list;
    }
}