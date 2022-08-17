using System.Collections.Generic;
using UnityEngine;

public class KillEnemyTask : LevelTask
{
    [SerializeField] private int _killAmount;
    [SerializeField] private List<GameObject> _enemyList;

    protected override void OnStart()
    {
        _taskType = TaskType.KillEnemies;

        _enemyList = new List<GameObject> (GameObject.FindGameObjectsWithTag(GameTags.Enemy));
        foreach(GameObject enemyObject in _enemyList)
        {
            enemyObject.GetComponent<Health>().Killed += OnEnemyKilled;
        }
        
        if(_killAmount == 0)
        {
            _killAmount = _enemyList.Count;
        }
    }

    private void OnEnemyKilled()
    {
        if(_status != TaskStatus.Completed)
        {
            _killAmount--;
            if (_killAmount <= 0)
            {
                CompleteTask();
            }
        }
    }
}
