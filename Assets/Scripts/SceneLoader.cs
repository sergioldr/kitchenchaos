using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    public enum Scene
    {
        MainMenuScene,
        GameScene,
        LoadingScene
    }
    private static Scene targetScene;

    public static void LoadScene(Scene targetScene)
    {
        SceneLoader.targetScene = targetScene;
        SceneManager.LoadScene(Scene.LoadingScene.ToString());
    }

    public static void SceneLoaderCallback()
    {
        SceneManager.LoadScene(targetScene.ToString());
    }
}
