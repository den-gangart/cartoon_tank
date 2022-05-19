using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShooter : MonoBehaviour
{
    [SerializeField] private GameObject _shootPoint;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _reoladTime = 1f;

    private bool _isReady = true;

    public void TryShoot()
    {
        if(_isReady == false)
        {
            return;
        }

        GameObject bulletObject = Instantiate(_bulletPrefab, _shootPoint.transform.position, transform.rotation);
        _isReady = false;
        StartCoroutine(Reolad());
    }

    private IEnumerator Reolad()
    {
        yield return new WaitForSeconds(_reoladTime);
        _isReady = true;
    }
}
