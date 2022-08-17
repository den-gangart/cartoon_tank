using UnityEngine;

[RequireComponent(typeof(LevelManager))]
public class LevelEventDispatcher : MonoBehaviour
{
    private LevelManager _levelManager;

    void Awake()
    {
        _levelManager = GetComponent<LevelManager>();
        _levelManager.TaskAdded += OnTaskAdded;
        _levelManager.TaskCompleted += OnTaskCompleted;
        _levelManager.LevelPassed += OnLevelPassed;
        _levelManager.LevelFailed += OnLevelFailed;
    }

    private void OnTaskAdded(LevelTask levelTask)
    {
        EventSystem.Broadcast(EContentEventType.TaskAdded, levelTask);
    }

    private void OnTaskCompleted(LevelTask levelTask)
    {
        EventSystem.Broadcast(EContentEventType.TaskCompleted, levelTask);
    }

    private void OnLevelPassed()
    {
        EventSystem.Broadcast(EContentEventType.Win);
    }

    private void OnLevelFailed()
    {
        EventSystem.Broadcast(EContentEventType.Lose);
    }
}
