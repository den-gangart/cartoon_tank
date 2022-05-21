using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShooter : MonoBehaviour
{
    [SerializeField] private GameObject _shootPoint;
    [SerializeField] private GameObject _bulletPrefab;

    [SerializeField] private GameObject _fxPoint;
    [SerializeField] private GameObject _shootFX;

    [SerializeField] private float _reoladTime = 1f;
    [SerializeField] private Animator _animator;

    private Action<Vector3> _shootAction;
    private bool _isReady = true;

    public void TryShoot()
    {
        if(_isReady == false)
        {
            return;
        }

        MakeShoot();
        StartCoroutine(Reolad());

        _shootAction.Invoke(transform.forward);
        _isReady = false;
    }

    private IEnumerator Reolad()
    {
        yield return new WaitForSeconds(_reoladTime);
        _isReady = true;
    }

    private void MakeShoot()
    {
        Instantiate(_bulletPrefab, _shootPoint.transform.position, transform.rotation);
        Instantiate(_shootFX, _fxPoint.transform.position, transform.rotation);

        if (_animator != null)
        {
            _animator.SetTrigger("Shoot");
        }
    }

    public void AddShootListener(Action<Vector3> evt)
    {
        _shootAction += evt;
    }

    private void OnDestroy()
    {
        _shootAction = null;
    }
}
