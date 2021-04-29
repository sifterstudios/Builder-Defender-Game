using BD.Building.SO;
using BD.Sound;
using Unity.Mathematics;
using UnityEngine;

namespace BD.Building
{
    public class BuildingConstruction : MonoBehaviour
    {
        float _constructionTimer;
        float _constructionTimerMax;
        BuildingTypeSO _buildingType;
        BoxCollider2D _boxCollider2D;
        SpriteRenderer _spriteRenderer;
        BuildingTypeHolder _buildingTypeHolder;
        Material _constructionMaterial;
        static readonly int Progress = Shader.PropertyToID("_Progress");

        void Awake()
        {
            _boxCollider2D = GetComponent<BoxCollider2D>();
            _spriteRenderer = transform.Find("sprite").GetComponent<SpriteRenderer>();
            _buildingTypeHolder = GetComponent<BuildingTypeHolder>();
            _constructionMaterial = _spriteRenderer.material;
        }

        public static BuildingConstruction Create(Vector3 position, BuildingTypeSO buildingType)
        {
            Transform pfBuildingConstruction = Resources.Load<Transform>("pfBuildingConstruction");
            Transform buildingConstructionTransform =
                Instantiate(pfBuildingConstruction, position, Quaternion.identity);

            BuildingConstruction buildingConstruction =
                buildingConstructionTransform.GetComponent<BuildingConstruction>();
            buildingConstruction.SetBuildingType(buildingType);
            return buildingConstruction;
        }

        void Update()
        {
            _constructionTimer -= Time.deltaTime;

            _constructionMaterial.SetFloat(Progress, GetConstructionTimerNormalized());
            if (_constructionTimer <= 0f)
            {
                Instantiate(_buildingType.prefab, transform.position, quaternion.identity);
                SoundManager.Instance.PlaySound(SoundManager.Sound.BuildingPlaced);
                Destroy(gameObject);
            }
        }

        void SetBuildingType(BuildingTypeSO buildingType)
        {
            _buildingType = buildingType;

            _constructionTimerMax = buildingType.constructionTimerMax;
            _constructionTimer = _constructionTimerMax;

            _spriteRenderer.sprite = buildingType.sprite;

            _boxCollider2D.offset = buildingType.prefab.GetComponent<BoxCollider2D>().offset;
            _boxCollider2D.size = buildingType.prefab.GetComponent<BoxCollider2D>().size;

            _buildingTypeHolder.buildingType = buildingType;
        }

        public float GetConstructionTimerNormalized()
        {
            return 1 - _constructionTimer / _constructionTimerMax;
        }
    }
}