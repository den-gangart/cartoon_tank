using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private List<LevelTask> _levelTaskList;
    [SerializeField] private float _winDelay = 0.5f;
    [SerializeField] private float _loseDelay = 0.5f;
    [SerializeField] private float _regularTimeScale = 1f;
    [SerializeField] private float _timeScaleOnEnd = 0.1f;
    private int _taskCount => _levelTaskList.Count;
    private bool _isLevelOnPause = false;
    private bool _isLevelFinished = false;

    public event Action<LevelTask> TaskAdded;
    public event Action<LevelTask> TaskCompleted;
    public event Action LevelPassed;
    public event Action LevelFailed;

    protected override void OnAwake()
    {
        base.OnAwake();
        Instance._levelTaskList = new List<LevelTask>();
        EventSystem.AddEventListener(EContentEventType.PlayerDead, OnPlayerDead);
        EventSystem.AddEventListener(EContentEventType.PauseButtonPressed, OnButtonPausePressed);
        Time.timeScale = _regularTimeScale;
    }

    public static void AddLevelTask(LevelTask levelTask)
    {
        if(Instance._levelTaskList.Contains(levelTask))
        {
            return;
        }

        Instance._levelTaskList.Add(levelTask);
        levelTask.Complete += Instance.OnTaskCompleted;
        Instance.TaskAdded?.Invoke(levelTask);
    }

    private static void RemoveLevelTask(LevelTask levelTask)
    {
        Instance._levelTaskList.Remove(levelTask);
        levelTask.Complete -= Instance.OnTaskCompleted;
    }

    private void OnTaskCompleted(LevelTask levelTask)
    {
        Instance.TaskCompleted?.Invoke(levelTask);
        RemoveLevelTask(levelTask);
        
        if(_taskCount == 0)
        {
            StartCoroutine(OnLevelPassed());
        }
    }

    private void OnPlayerDead()
    {
        StartCoroutine(OnLevelFailed());
    }

    private IEnumerator OnLevelPassed()
    {
        Time.timeScale = _timeScaleOnEnd;
        yield return new WaitForSeconds(_winDelay);
        Instance.LevelPassed?.Invoke();
        EventSystem.Broadcast(EContentEventType.StopBackgroundMusic);
        _isLevelFinished = true;
    }

    private IEnumerator OnLevelFailed()
    {
        Time.timeScale = _timeScaleOnEnd;
        yield return new WaitForSeconds(_loseDelay);
        Instance.LevelFailed?.Invoke();
        EventSystem.Broadcast(EContentEventType.StopBackgroundMusic);
        _isLevelFinished = true;
    }

    private void OnButtonPausePressed()
    {
        if(_isLevelOnPause)
        {
            EventSystem.Broadcast(EContentEventType.GameResume);
            Time.timeScale = _regularTimeScale;
            _isLevelOnPause = false;
        }
        else if(_isLevelFinished == false)
        {
            EventSystem.Broadcast(EContentEventType.GamePause);
            Time.timeScale = 0;
            _isLevelOnPause = true;
        }
    }

    private void OnDestroy()
    {
        EventSystem.RemoveEventListener(EContentEventType.PlayerDead, OnPlayerDead);
        EventSystem.RemoveEventListener(EContentEventType.PauseButtonPressed, OnButtonPausePressed);
    }
}
