using UnityEngine.SceneManagement;

namespace BD.Utilities
{
    public static class GameSceneManager
    {
        public enum Scene
        {
            GameScene,
            MainMenuScene
        }
        public static void Load(Scene scene)
        {
            SceneManager.LoadScene(scene.ToString());
        }
    }
}
