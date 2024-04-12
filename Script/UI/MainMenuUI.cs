using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playGame;
    [SerializeField] private Button quitGame;
    private void Awake()
    {
        playGame.onClick.AddListener(() =>
        {
           Loader.LoadTagetScene(Loader.Scene.kitchen);
        });
        quitGame.onClick.AddListener(() =>
        {
            Application.Quit();
        });
        Time.timeScale = 1.0f;
        playGame.Select();
    }


}