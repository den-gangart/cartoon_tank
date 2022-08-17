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
    [SerializeField] private List<MeshRenderer> _tankMeshComponents;
    [SerializeField] private TankMovement _tankMovement;
    [SerializeField] private GunShooter _gunShooter;
    [SerializeField] private GameObject _destroyFireFX;
    [SerializeField] private GameObject _destroyExplosionFX;
    [SerializeField] private Material _destroyMaterial;
    [SerializeField] private string _explosionSound;

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
                ApplyDestroyFX();
                break;
        }
    }

    private void ApplyDestroyFX()
    {
        if(string.IsNullOrEmpty(_explosionSound) == false)
        {
            AudioHandler.PlayGameSound(_explosionSound, gameObject);
        }

        foreach(MeshRenderer tankComponent in _tankMeshComponents)
        {
            tankComponent.material = _destroyMaterial;
        }

        Instantiate(_destroyFireFX, transform);
        Instantiate(_destroyExplosionFX, transform);
    }
}
