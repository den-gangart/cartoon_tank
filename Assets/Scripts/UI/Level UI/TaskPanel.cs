using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskPanel : MonoBehaviour
{
    [SerializeField] private GameObject _taskPanelElement;
    private List<TaskPanelComponent> _taskPanelComponentList = new List<TaskPanelComponent>();

    void Awake()
    {
        EventSystem.AddEventListener<LevelTask>(EContentEventType.TaskAdded, OnTaskAdded);
    }

    private void OnTaskAdded(LevelTask levelTask)
    {
        CreateTaskComponent(levelTask);
    }

    private void CreateTaskComponent(LevelTask levelTask)
    {
        TaskPanelComponent taskComponent = Instantiate(_taskPanelElement, transform).GetComponent<TaskPanelComponent>();
        taskComponent.Initialize(levelTask);
        _taskPanelComponentList.Add(taskComponent);
    }

    private void OnDestroy()
    {
        EventSystem.RemoveEventListener<LevelTask>(EContentEventType.TaskAdded, OnTaskAdded);
    }
}
