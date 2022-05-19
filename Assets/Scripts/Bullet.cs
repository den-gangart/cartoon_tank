using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float  _bulletSpeed = 50f;
    [SerializeField] private float _maxLiveTime = 5f;

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
        Destroy(gameObject);
    }

    private IEnumerator DestroyOnMiss()
    {
        yield return new WaitForSeconds(_maxLiveTime);
        Destroy(gameObject);
    }
}
