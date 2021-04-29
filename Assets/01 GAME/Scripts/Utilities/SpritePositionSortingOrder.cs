using UnityEngine;

public class SpritePositionSortingOrder : MonoBehaviour
{
    [SerializeField] bool runOnce;
    [SerializeField] float positionOffsetY;
    SpriteRenderer _spriteRenderer;
    float _precisionMultiplier = 5f;


    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void LateUpdate()
    {
        _spriteRenderer.sortingOrder = (int) (-(transform.position.y + positionOffsetY)* _precisionMultiplier);

        if (runOnce)
        {
            Destroy(this);
        }
    }
}