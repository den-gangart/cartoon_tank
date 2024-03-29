using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private PopupWindow _settingsPopup;
    [SerializeField] private Animator _mainAnimator;
    [SerializeField] private float _regularTimeScale = 1f;

    void Start()
    {
        _startButton?.onClick.AddListener(OnStartClick);
        _settingsButton?.onClick.AddListener(OnSettingClick);
        _exitButton?.onClick.AddListener(OnExitClick);

        Time.timeScale = _regularTimeScale;
    }

    private void OnStartClick()
    {
        SceneManager.LoadSceneAsync(SceneNames.LEVEL_1);
    }

    private void OnSettingClick()
    {
        HideMenu();
        PopupWindow popupWindow = Instantiate(_settingsPopup, transform);
        popupWindow.OnPopupClosed.AddListener(ShowMenu);
    }

    private void OnExitClick()
    {
        Application.Quit();
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
