using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float  _bulletSpeed = 50f;
    [SerializeField] private float _maxLiveTime = 5f;
    [SerializeField] private GameObject _vfxPoint;
    [SerializeField] private GameObject _explosionVFX;

    private void Start()
    {
        StartCoroutine(DestroyOnMiss());
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * _bulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Instantiate(_explosionVFX, _vfxPoint.transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private IEnumerator DestroyOnMiss()
    {
        yield return new WaitForSeconds(_maxLiveTime);
        Destroy(gameObject);
    }
}
