using BD.Sound;
using Building.SO;
using Unity.Mathematics;
using UnityEngine;

namespace Building
{
    public class BuildingConstruction : MonoBehaviour
    {
        static readonly int Progress = Shader.PropertyToID("_Progress");
        BoxCollider2D _boxCollider2D;
        BuildingTypeSO _buildingType;
        BuildingTypeHolder _buildingTypeHolder;
        Material _constructionMaterial;
        float _constructionTimer;
        float _constructionTimerMax;
        SpriteRenderer _spriteRenderer;

        void Awake()
        {
            _boxCollider2D = GetComponent<BoxCollider2D>();
            _spriteRenderer = transform.Find("sprite").GetComponent<SpriteRenderer>();
            _buildingTypeHolder = GetComponent<BuildingTypeHolder>();
            _constructionMaterial = _spriteRenderer.material;

            Instantiate(Resources.Load<Transform>("pfBuildingPlacedParticles"),
                transform.position, Quaternion.identity);
        }

        void Update()
        {
            _constructionTimer -= Time.deltaTime;

            _constructionMaterial.SetFloat(Progress, GetConstructionTimerNormalized());
            if (_constructionTimer <= 0f)
            {
                Instantiate(_buildingType.prefab, transform.position, quaternion.identity);
                Instantiate(Resources.Load<Transform>("pfBuildingPlacedParticles"),
                    transform.position, Quaternion.identity);

                SoundManager.Instance.PlaySound(SoundManager.Sound.BuildingPlaced);
                Destroy(gameObject);
            }
        }

        public static BuildingConstruction Create(Vector3 position, BuildingTypeSO buildingType)
        {
            var pfBuildingConstruction = Resources.Load<Transform>("pfBuildingConstruction");
            var buildingConstructionTransform =
                Instantiate(pfBuildingConstruction, position, Quaternion.identity);

            var buildingConstruction =
                buildingConstructionTransform.GetComponent<BuildingConstruction>();
            buildingConstruction.SetBuildingType(buildingType);
            return buildingConstruction;
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