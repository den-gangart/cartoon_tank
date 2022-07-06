using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _exitButton;

    void Start()
    {
        _startButton?.onClick.AddListener(OnStartClick);
        _settingsButton?.onClick.AddListener(OnSettingClick);
        _exitButton?.onClick.AddListener(OnExitClick);
    }

    private void OnStartClick()
    {
        SceneManager.LoadScene(SceneNames.LEVEL_1);
    }

    private void OnSettingClick()
    {

    }

    private void OnExitClick()
    {
        Application.Quit();
    }
}
