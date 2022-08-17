using System;
using System.Collections;
using UnityEngine;

public class GunShooter : MonoBehaviour
{
    [SerializeField] private GameObject _shootPoint;
    [SerializeField] private GameObject _bulletPrefab;

    [SerializeField] private GameObject _fxPoint;
    [SerializeField] private GameObject _shootFX;

    [SerializeField] private float _reoladTime = 1f;
    [SerializeField] private Animator _animator;

    [SerializeField] private string _shootSound = "Explosion";
    [SerializeField] private string _reoladSound;

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

        if (string.IsNullOrEmpty(_reoladSound) == false)
        {
            AudioHandler.PlayGameSound(_reoladSound, gameObject);
        }

        _isReady = true;
    }

    private void MakeShoot()
    {
        Instantiate(_bulletPrefab, _shootPoint.transform.position, transform.rotation);
        Instantiate(_shootFX, _fxPoint.transform.position, transform.rotation);

        if (string.IsNullOrEmpty(_shootSound) == false)
        {
            AudioHandler.PlayGameSound(_shootSound, gameObject);
        }

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
