using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get;private set; }
    public event EventHandler OnInteract;
    public event EventHandler OnInteractAlternate;
    public event EventHandler OnPauseGame;
    private PlayerInputAction action;
    private const string PLAYER_PREF_PLAYER_INPUT_ACTION = "PlayerPrefPlayerInputAction";
    public enum Binding
    {
        Move_Up,
        Move_Left,
        Move_Right,
        Move_Down,
        Interact,
        Interact_Alternate,
        Pause,
        InteractGamepad,
        Interact_AlternateGamepad,
        PauseGamepad
    }
    private void Awake()
    {
        Instance = this;
        action = new PlayerInputAction();
        if (PlayerPrefs.HasKey(PLAYER_PREF_PLAYER_INPUT_ACTION))
        {
            action.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREF_PLAYER_INPUT_ACTION));
        }
        action.Player.Enable();
    }
    private void Update()
    {
        action.Player.Interactions.performed += Interactions_performed;
        action.Player.InteractAlternate.performed += InteractAlternate_performed;
        action.Player.Pause.performed += Pause_performed;
    }
    private void OnDestroy()
    {
        action.Player.Interactions.performed -= Interactions_performed;
        action.Player.InteractAlternate.performed -= InteractAlternate_performed;
        action.Player.Pause.performed -= Pause_performed;
        action.Dispose();
    }

    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPauseGame?.Invoke(this, EventArgs.Empty);
    }

    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAlternate?.Invoke(this,EventArgs.Empty);
    }

    private void Interactions_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteract?.Invoke(this,EventArgs.Empty);
    }

    public Vector2 MoveInputNormalize()
    {
        Vector2 inputVector2 = action.Player.Keyboard.ReadValue<Vector2>();
        inputVector2.Normalize();
        return inputVector2;
    }
    public string GetBindingString(Binding binding)
    {
        switch (binding)
        {
            default:
            case Binding.Move_Up:
                return action.Player.Keyboard.bindings[1].ToDisplayString();
            case Binding.Move_Down:
                return action.Player.Keyboard.bindings[2].ToDisplayString();
            case Binding.Move_Left:
                return action.Player.Keyboard.bindings[3].ToDisplayString();
            case Binding.Move_Right:
                return action.Player.Keyboard.bindings[4].ToDisplayString();
            case Binding.Interact:
                return action.Player.Interactions.bindings[0].ToDisplayString();
            case Binding.Interact_Alternate:
                return action.Player.InteractAlternate.bindings[0].ToDisplayString();
            case Binding.Pause:
                return action.Player.Pause.bindings[0].ToDisplayString();
            case Binding.InteractGamepad:
                return action.Player.Interactions.bindings[1].ToDisplayString();
            case Binding.Interact_AlternateGamepad:
                return action.Player.InteractAlternate.bindings[1].ToDisplayString();
            case Binding.PauseGamepad:
                return action.Player.Pause.bindings[1].ToDisplayString();
        }
    }
    public void RebindBinding(Binding binding,Action aHidePressToRebind)
    {
        InputAction inputAction;
        int indexRebind;
        
        switch (binding)
        {
            default:
                case Binding.Move_Up:
                inputAction = action.Player.Keyboard;
                indexRebind = 1;
                break;
            case Binding.Move_Down:
                inputAction = action.Player.Keyboard;
                indexRebind = 2;
                break;
            case Binding.Move_Left:
                inputAction = action.Player.Keyboard;
                indexRebind = 3;
                break;
            case Binding.Move_Right:
                inputAction = action.Player.Keyboard;
                indexRebind = 4;
                break;
            case Binding.Interact:
                inputAction = action.Player.Interactions;
                indexRebind = 0;
                break;
            case Binding.Interact_Alternate:
                inputAction = action.Player.InteractAlternate;
                indexRebind = 0;
                break;
            case Binding.Pause:
                inputAction = action.Player.Pause;
                indexRebind = 0;
                break;
            case Binding.InteractGamepad:
                inputAction = action.Player.Interactions;
                indexRebind = 1;
                break;
            case Binding.Interact_AlternateGamepad:
                inputAction = action.Player.InteractAlternate;
                indexRebind = 1;
                break;
            case Binding.PauseGamepad:
                inputAction = action.Player.Pause;
                indexRebind = 1;
                break;
        }
        action.Player.Disable();
        inputAction.PerformInteractiveRebinding(indexRebind)
            .OnComplete(callback =>
            {
                callback.Dispose();
                action.Player.Enable();
                aHidePressToRebind();
                PlayerPrefs.SetString(PLAYER_PREF_PLAYER_INPUT_ACTION, action.SaveBindingOverridesAsJson());
                PlayerPrefs.Save();
            }).Start();
       ;
    }
}
