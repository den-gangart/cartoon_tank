using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausePopup : MonoBehaviour
{
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _backToMenuButton;
    [SerializeField] private PopupWindow _settingsPopup;
    [SerializeField] private Animator _mainAnimator;

    void Start()
    {
        _resumeButton?.onClick.AddListener(OnResumeClick);
        _settingsButton?.onClick.AddListener(OnSettingClick);
        _backToMenuButton?.onClick.AddListener(OnBackToMenuClick);
    }

    private void OnBackToMenuClick()
    {
        SceneManager.LoadSceneAsync(SceneNames.MAIN_MENU);
    }

    private void OnSettingClick()
    {
        HideMenu();
        PopupWindow popupWindow = Instantiate(_settingsPopup, transform);
        popupWindow.OnPopupClosed.AddListener(ShowMenu);
    }

    private void OnResumeClick()
    {
        EventSystem.Broadcast(EContentEventType.PauseButtonPressed);
    }

    public void HideMenu()
    {
        _mainAnimator.SetTrigger("Hide");
    }

    public void ShowMenu()
    {
        _mainAnimator.SetTrigger("Show");
    }
}
