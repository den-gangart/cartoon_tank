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
        EventSystem.AddEventListener(EContentEventType.GamePause, OnGamePause);
        EventSystem.AddEventListener(EContentEventType.Win, OnGamePause);
        EventSystem.AddEventListener(EContentEventType.Lose, OnGamePause);
        EventSystem.AddEventListener(EContentEventType.GameResume, OnGameResume);
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

    private void OnDestroy()
    {
        EventSystem.RemoveEventListener(EContentEventType.GamePause, OnGamePause);
        EventSystem.RemoveEventListener(EContentEventType.GameResume, OnGameResume);
        EventSystem.RemoveEventListener(EContentEventType.Lose, OnGamePause);
        EventSystem.RemoveEventListener(EContentEventType.Win, OnGamePause);
    }
}
