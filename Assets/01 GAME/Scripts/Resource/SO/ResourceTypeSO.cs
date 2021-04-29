using UnityEngine;

namespace BD.Resource.SO
{
    [CreateAssetMenu(menuName = "ScriptableObjects/ResourceType")]
    public class ResourceTypeSO : ScriptableObject
    {
        public string nameString;
        public string nameShort;
        public Sprite sprite;
        public string colorHex;

    }
}
