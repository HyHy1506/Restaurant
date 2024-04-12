using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader 
{
    public enum Scene
    {
        kitchen,
        MainMenuScene,
        Loader
    }
    private static Scene tagetScene;
    public static void LoadTagetScene(Scene scene)
    {
        Loader.tagetScene = scene;
        SceneManager.LoadScene(Loader.Scene.Loader.ToString());
    }
    public static void LoaderCallBack()
    {
        SceneManager.LoadScene(Loader.tagetScene.ToString());
    }
}
