using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioHandler))]
public class SceneBackgroundSound : MonoBehaviour
{
    [SerializeField] private List<SceneBackgroundSoundSettings> _sceneSoundsSettingList;

    private SceneBackgroundSoundSettings _currentSceneSettings;
    private AudioSourceHandler _currentSourceHandler;
    private int _currentSoundIndex = 0;

    private void Start()
    {
        EventSystem.AddEventListener(EContentEventType.GamePause, OnGamePaused);
        EventSystem.AddEventListener(EContentEventType.GameResume, OnGameResumed);
        EventSystem.AddEventListener(EContentEventType.StopBackgroundMusic, OnStop);
        EventSystem.AddEventListener(EContentEventType.PlayBackgroundMusic, OnPlay);
        SceneManager.sceneLoaded += OnSceneLoaded;

        SetCurrentSceneSettings(SceneManager.GetActiveScene().name);
        PlayCurrentSound();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if(_currentSourceHandler != null)
        {
            OnStop();
        }

        SetCurrentSceneSettings(scene.name);
        PlayCurrentSound();
    }

    private void SetCurrentSceneSettings(string sceneName)
    {
        foreach(var sceneSettings in _sceneSoundsSettingList)
        {
            if (sceneName.Equals(sceneSettings.GetName()))
            {
                _currentSceneSettings = sceneSettings;
                break;
            }
        }
    }

    private void OnBackgroundSoundStopped(AudioSourceHandler source)
    {
        source.SoundStopped -= OnBackgroundSoundStopped;
        PlayCurrentSound();
    }

    private void OnGamePaused()
    {
        _currentSourceHandler?.Pause();
    }

    private void OnGameResumed()
    {
        _currentSourceHandler?.Resume();
    }
    
    private void OnStop()
    {
        if (_currentSourceHandler != null)
        {
            _currentSourceHandler.SoundStopped -= OnBackgroundSoundStopped;
            _currentSourceHandler.Stop();
        }
    }

    private void OnPlay()
    {
        OnStop();
        PlayCurrentSound();
    }

    private void PlayCurrentSound()
    {
        int soundCount = _currentSceneSettings.GetSoundCount();

        switch (_currentSceneSettings.GetSoundSequenceType())
        {
            case SoundSequenceType.Random:
                _currentSoundIndex = Random.Range(0, soundCount);
                break;
            case SoundSequenceType.Successively:
                _currentSoundIndex++;

                if(_currentSoundIndex >= soundCount)
                {
                    _currentSoundIndex = 0;
                }
                break;
        }

        string currentSound = _currentSceneSettings.GetSound(_currentSoundIndex);
        _currentSourceHandler = AudioHandler.PlayGameSound(currentSound, gameObject);
        _currentSourceHandler.SoundStopped += OnBackgroundSoundStopped;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        EventSystem.RemoveEventListener(EContentEventType.GamePause, OnGamePaused);
        EventSystem.RemoveEventListener(EContentEventType.GameResume, OnGameResumed);
        EventSystem.RemoveEventListener(EContentEventType.StopBackgroundMusic, OnStop);
        EventSystem.RemoveEventListener(EContentEventType.PlayBackgroundMusic, OnPlay);
    }
}