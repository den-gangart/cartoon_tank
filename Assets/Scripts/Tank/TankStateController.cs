using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ETankState
{
    Alive,
    Destroed,
}

public class TankStateController : MonoBehaviour
{
    [SerializeField] private ETankState _currentState;
    [SerializeField] private TankMovement _tankMovement;
    [SerializeField] private GunShooter _gunShooter;

    public void UpdateState(ETankState newState)
    {
        if(_currentState == newState)
        {
            return;
        }

        _currentState = newState;
        OnStateUpdated();
    }

    public bool isAlive()
    {
        return _currentState == ETankState.Alive;
    }

    private void OnStateUpdated()
    {
        switch (_currentState)
        {
            case ETankState.Alive:
                _tankMovement.EnableMovement();
                _gunShooter.EnableShoot();
                break;
            case ETankState.Destroed:
                _tankMovement.DisableMovement();
                _gunShooter.DisableShoot();
                break;
        }
    }
}
