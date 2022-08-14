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

    public event EventHandler<Vector3> GunShoot;
    private bool _isReady = true;
    private bool _canShoot = true;

    public void TryShoot()
    {
        if (_canShoot == false || _isReady == false)
        {
            return;
        }

        MakeShoot();
        StartCoroutine(Reolad());

        GunShoot?.Invoke(this, transform.forward);
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

        AudioHandler.PlayGameSound("Explosion", gameObject);

        if (_animator != null)
        {
            _animator.SetTrigger("Shoot");
        }
    }

    public void EnableShoot()
    {
        _canShoot = true;
    }

    public void DisableShoot()
    {
        _canShoot = false;
    }

    public bool isReolading()
    {
        return !_isReady;
    }
}
