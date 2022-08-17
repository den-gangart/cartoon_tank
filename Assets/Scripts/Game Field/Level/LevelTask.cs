using System;
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
    public string Description { get { return _description; } }

    [SerializeField] protected TaskStatus _status = TaskStatus.InProgress;
    [SerializeField] protected string _description;

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
