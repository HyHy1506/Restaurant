using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance { get; private set; }
    [SerializeField] private Button soundButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI textSound;
    [SerializeField] private TextMeshProUGUI textMusic;
    [SerializeField] private TextMeshProUGUI textMoveUp;
    [SerializeField] private TextMeshProUGUI textMoveLeft;
    [SerializeField] private TextMeshProUGUI textMoveRight;
    [SerializeField] private TextMeshProUGUI textMoveDown;
    [SerializeField] private TextMeshProUGUI textInteract;
    [SerializeField] private TextMeshProUGUI textInteractAlternate;
    [SerializeField] private TextMeshProUGUI textPause;
    [SerializeField] private TextMeshProUGUI textInteractGamepad;
    [SerializeField] private TextMeshProUGUI textInteractAlternateGamepad;
    [SerializeField] private TextMeshProUGUI textPauseGamepad;

    [SerializeField] private Button buttonMoveUp;
    [SerializeField] private Button buttonMoveLeft;
    [SerializeField] private Button buttonMoveRight;
    [SerializeField] private Button buttonMoveDown;
    [SerializeField] private Button buttonInteract;
    [SerializeField] private Button buttonInteractAlternate;
    [SerializeField] private Button buttonPause;
    [SerializeField] private Button buttonInteractGamepad;
    [SerializeField] private Button buttonInteractAlternateGamepad;
    [SerializeField] private Button buttonPauseGamepad;
    [SerializeField] private Transform pressToRebind;


    private void Awake()
    {
        Instance = this;
        soundButton.onClick.AddListener(() => {
            SoundManager.Instance.InscreaseSoundVolume();
            UpdateVisual();
        });
        musicButton.onClick.AddListener(() => {
            MusicManager.Instance.InscreaseMusicVolume();
            UpdateVisual();
        });
        closeButton.onClick.AddListener(() => {
            GamePauseUI.Instance.Show();
            Hide();
        });
        buttonMoveUp.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Move_Up);
        });
        buttonMoveDown.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Move_Down);
        });
        buttonMoveLeft.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Move_Left);
        });
        buttonMoveRight.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Move_Right);
        });
        buttonInteract.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Interact);
        });
        buttonInteractAlternate.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Interact_Alternate);
        });
        buttonPause.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Pause);
        });
        buttonInteractGamepad.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.InteractGamepad);
        });
        buttonInteractAlternateGamepad.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Interact_AlternateGamepad);
        });
        buttonPauseGamepad.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.PauseGamepad);
        });

    }
    private void Start()
    {
        HidePressToRebind();
        Hide();
        UpdateVisual();
        GameManager.Instance.OnResumeGame += GameManager_OnResumeGame;
    }

    private void GameManager_OnResumeGame(object sender, System.EventArgs e)
    {
        Hide();

    }


    private void UpdateVisual()
    {
        textSound.text = "Sound Volume : " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f);
        textMusic.text = "Music Volume : " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);
        textMoveUp.text = GameInput.Instance.GetBindingString(GameInput.Binding.Move_Up);
        textMoveLeft.text = GameInput.Instance.GetBindingString(GameInput.Binding.Move_Left);
        textMoveRight.text = GameInput.Instance.GetBindingString(GameInput.Binding.Move_Right);
        textMoveDown.text = GameInput.Instance.GetBindingString(GameInput.Binding.Move_Down);
        textInteract.text = GameInput.Instance.GetBindingString(GameInput.Binding.Interact);
        textInteractAlternate.text = GameInput.Instance.GetBindingString(GameInput.Binding.Interact_Alternate);
        textPause.text = GameInput.Instance.GetBindingString(GameInput.Binding.Pause);
        textInteractGamepad.text = GameInput.Instance.GetBindingString(GameInput.Binding.InteractGamepad);
        textInteractAlternateGamepad.text = GameInput.Instance.GetBindingString(GameInput.Binding.Interact_AlternateGamepad);
        textPauseGamepad.text = GameInput.Instance.GetBindingString(GameInput.Binding.PauseGamepad);

    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    public void Show()
    {
        gameObject.SetActive(true);
        soundButton.Select();
    }
    private void HidePressToRebind()
    {
        pressToRebind.gameObject.SetActive(false);
    }
    private void ShowPressToRebind()
    {
        pressToRebind.gameObject.SetActive(true);
    }
    private void RebindBinding(GameInput.Binding binding)
    {
        ShowPressToRebind();
        GameInput.Instance.RebindBinding(binding, () =>
        {
            UpdateVisual();
            HidePressToRebind();
        });

    }
}
