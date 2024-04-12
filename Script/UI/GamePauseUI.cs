using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    public static GamePauseUI Instance { get; private set; }
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button optionsButton;

    private void Awake()
    {
        Instance = this;
        resumeButton.onClick.AddListener(() => { 
            GameManager.Instance.togglePauseGame();
        });
        mainMenuButton.onClick.AddListener(() => {
            Loader.LoadTagetScene(Loader.Scene.MainMenuScene);
        });
        optionsButton.onClick.AddListener(() => {
            OptionsUI.Instance.Show();
            Hide();
        });
    }
    private void Start()
    {
        GameManager.Instance.OnPauseGame += GameManager_OnPauseGame;
        GameManager.Instance.OnResumeGame += GameManager_OnResumeGame;
        Hide();
    }

    private void GameManager_OnResumeGame(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void GameManager_OnPauseGame(object sender, System.EventArgs e)
    {
        Show();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        resumeButton.Select();
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}