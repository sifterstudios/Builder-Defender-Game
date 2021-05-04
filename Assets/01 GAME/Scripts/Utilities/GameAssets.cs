using UnityEngine;

namespace BD.Utilities
{
    public class GameAssets : MonoBehaviour
    {
        static GameAssets instance;

        public static GameAssets Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = Resources.Load<GameAssets>("GameAssets");
                }

                return instance;
            }

        }

        public Transform pfEnemy;
        public Transform pfArrowProjectile;
        public Transform pfBuildingDestroyedParticles;
        public Transform pfBuildingPlacedParticles;
        public Transform pfBuildingConstruction;
        public Transform pfEnemyDieParticles;
    }
}