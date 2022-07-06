using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private List<LevelTask> _levelTaskList;
    [SerializeField] private float _winDelay = 0.5f;
    [SerializeField] private float _loseDelay = 0.5f;

    private int _taskCount => _levelTaskList.Count;

    protected override void OnAwake()
    {
        base.OnAwake();
        Instance._levelTaskList = new List<LevelTask>();
        EventSystem.AddEventListener(EContentEventType.PlayerDead, OnPlayerDead);
    }

    public static void AddLevelTask(LevelTask levelTask)
    {
        if(Instance._levelTaskList.Contains(levelTask))
        {
            return;
        }

        Instance._levelTaskList.Add(levelTask);
        levelTask.Complete += Instance.OnTaskCompleted;
    }

    private static void RemoveLevelTask(LevelTask levelTask)
    {
        Instance._levelTaskList.Remove(levelTask);
        levelTask.Complete -= Instance.OnTaskCompleted;
    }

    private void OnTaskCompleted(LevelTask levelTask)
    {
        EventSystem.Broadcast(EContentEventType.TaskCompleted, levelTask);
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
        yield return new WaitForSeconds(_winDelay);
        EventSystem.Broadcast(EContentEventType.Win);
    }

    private IEnumerator OnLevelFailed()
    {
        yield return new WaitForSeconds(_loseDelay);
        EventSystem.Broadcast(EContentEventType.Lose);
    }

    private void OnDestroy()
    {
        EventSystem.RemoveEventListener(EContentEventType.PlayerDead, OnPlayerDead);
    }
}
