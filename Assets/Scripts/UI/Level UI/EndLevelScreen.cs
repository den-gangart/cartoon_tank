using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndLevelScreen : MonoBehaviour
{
    [SerializeField] private GameObject _winTextObject;
    [SerializeField] private GameObject _loseTextObject;
    [SerializeField] private GameObject _anchor;
    [SerializeField] private Button _backToMenuButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private string _winSound;
    [SerializeField] private string _loseSound;

    void Awake()
    {
        _anchor.SetActive(false);

        EventSystem.AddEventListener(EContentEventType.Win, OnLevelPassed);
        EventSystem.AddEventListener(EContentEventType.Lose, OnLevelFailed);

        _backToMenuButton.onClick.AddListener(OnBackToMenu);
        _restartButton.onClick.AddListener(OnRestartLevel);
    }

    private void OnLevelPassed()
    {
        ShowEndLevelScreen(true);
        AudioHandler.PlayGameSound(_winSound, gameObject);
    }

    private void OnLevelFailed()
    {
        ShowEndLevelScreen(false);
        AudioHandler.PlayGameSound(_loseSound, gameObject);
    }

    private void ShowEndLevelScreen(bool isWin)
    {
        _anchor.SetActive(true);
        _winTextObject.SetActive(isWin);
        _loseTextObject.SetActive(!isWin);
    }

    private void OnBackToMenu()
    {
        SceneManager.LoadScene(SceneNames.MAIN_MENU);
    }

    private void OnRestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnDestroy()
    {
        EventSystem.RemoveEventListener(EContentEventType.Win, OnLevelPassed);
        EventSystem.RemoveEventListener(EContentEventType.Lose, OnLevelFailed);
    }
}
