using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    [SerializeField] private TankStateController _tankState;
    [SerializeField] private float _maxHealth;

    private float _healthPoints;

    private void Start()
    {
        _healthPoints = _maxHealth;
    }

    public void AddHealth(float amount)
    {
       if((_healthPoints + amount) > _maxHealth)
       {
            amount = _maxHealth - _healthPoints;
       }

        _healthPoints += amount;
    }

    public void TakeDamage(float amount)
    {
        if(_healthPoints < amount)
        {
            amount = _healthPoints;
        }

        _healthPoints -= amount;
    }
    
    public void Kill()
    {
        _healthPoints = 0;
        _tankState.UpdateState(ETankState.Destroed);
    }

    public bool isAlive()
    {
        return _healthPoints > 0;
    }
}
