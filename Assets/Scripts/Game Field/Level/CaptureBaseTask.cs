using UnityEngine;

[RequireComponent(typeof(BaseCapture))]
public class CaptureBaseTask : LevelTask
{
    private BaseCapture _baseCapture;

    protected override void OnStart()
    {
        _taskType = TaskType.CaptureBase;
        _baseCapture = GetComponent<BaseCapture>();
        _baseCapture.CaptureCompleted += OnCompleted;
    }

    protected override void OnCompleted()
    {
        CompleteTask();
        _baseCapture.CaptureCompleted -= OnCompleted;
    }
}
