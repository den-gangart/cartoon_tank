using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorState : MonoBehaviour
{
    [SerializeField] private ECursorState _defaultState;
    private ECursorState _currentState;

    private enum ECursorState
    {
        Hidden,
        Active,
    }

    private void Start()
    {
        UpdateState(_defaultState);
    }

    private void OnGamePause()
    {
        UpdateState(ECursorState.Active);
    }

    private void OnGameResume()
    {
        UpdateState(ECursorState.Hidden);
    }

    private void UpdateState(ECursorState newState)
    {
        _currentState = newState;
        switch(_currentState)
        {
            case ECursorState.Hidden:
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                break;
            case ECursorState.Active:
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                break;
        }
    }

    private void OnEnable()
    {
        UIEventSystem.AddUIEventListener(EUIEvent.GamePause, OnGamePause);
        UIEventSystem.AddUIEventListener(EUIEvent.GameResume, OnGameResume);
    }

    private void OnDisable()
    {
        UIEventSystem.RemoveUIEventListener(EUIEvent.GamePause, OnGamePause);
        UIEventSystem.RemoveUIEventListener(EUIEvent.GameResume, OnGameResume);
    }
}
