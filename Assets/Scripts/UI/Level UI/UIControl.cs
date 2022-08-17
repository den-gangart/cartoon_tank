using UnityEngine;

public class UIControl : MonoBehaviour
{
    [SerializeField] private GameObject _levelCanvas;
    [SerializeField] private PopupWindow _pausePopup;
    private PopupWindow _activePopup;

    private void Awake()
    {
        EventSystem.AddEventListener(EContentEventType.GamePause, OnLevelPause);
        EventSystem.AddEventListener(EContentEventType.GameResume, OnLevelResume);
    }

    private void OnLevelPause()
    {
        _activePopup = Instantiate(_pausePopup, transform);
        _levelCanvas.SetActive(false);
    }

    private void OnLevelResume()
    {
        _activePopup.DestroyPopup();
        _levelCanvas.SetActive(true);
    }

    private void OnDestroy()
    {
        EventSystem.RemoveEventListener(EContentEventType.GamePause, OnLevelPause);
        EventSystem.RemoveEventListener(EContentEventType.GameResume, OnLevelResume);
    }
}
