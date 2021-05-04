using UnityEngine;

namespace Utilities
{
    public class SpritePositionSortingOrder : MonoBehaviour
    {
        [SerializeField] bool runOnce;
        [SerializeField] float positionOffsetY;
        readonly float _precisionMultiplier = 5f;
        SpriteRenderer _spriteRenderer;


        void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        void LateUpdate()
        {
            _spriteRenderer.sortingOrder = (int) (-(transform.position.y + positionOffsetY) * _precisionMultiplier);

            if (runOnce) Destroy(this);
        }
    }
}