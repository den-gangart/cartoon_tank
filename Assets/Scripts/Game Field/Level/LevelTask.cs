using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TaskStatus
{
   InProgress,
   Completed,
}

public enum TaskType
{
    CaptureBase,
    KillEnemies,
}

public class LevelTask : MonoBehaviour
{
    public event Action<LevelTask> Complete;

    [SerializeField] protected TaskStatus _status = TaskStatus.InProgress;
    protected TaskType _taskType;

    private void Start()
    {
        LevelManager.AddLevelTask(this);
        OnStart();
    }

    protected void CompleteTask()
    {
        Complete?.Invoke(this);
        _status = TaskStatus.Completed;
    }

    protected virtual void OnStart() { }
    protected virtual void OnCompleted() { }
}
