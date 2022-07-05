using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private List<LevelTask> _levelTaskList;

    protected override void OnAwake()
    {
        base.OnAwake();
        Instance._levelTaskList = new List<LevelTask>();
    }

    public static void AddLevelTask(LevelTask levelTask)
    {
        Instance._levelTaskList.Add(levelTask);
    }
}
