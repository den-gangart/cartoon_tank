using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    [SerializeField] private TankStateController _tankState;
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _healthPoints;

    public event EventHandler<float> HealthChange;
    public event Action Killed;

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
        HealthChange?.Invoke(this, _healthPoints);
    }

    public void TakeDamage(float amount)
    {
        if (_healthPoints - amount <= 0)
        {
            if (_healthPoints != 0)
            {
                Kill();
            }
            return;
        }

        _healthPoints -= amount;
        HealthChange?.Invoke(this, _healthPoints);
    }
    
    public void Kill()
    {
        _healthPoints = 0;
        _tankState.UpdateState(ETankState.Destroed);

        HealthChange?.Invoke(this, _healthPoints);
        Killed?.Invoke();
    }

    public float GetHealthInPercent()
    {
        return _healthPoints / _maxHealth;
    }

    public bool isAlive()
    {
        return _healthPoints > 0;
    }
}
