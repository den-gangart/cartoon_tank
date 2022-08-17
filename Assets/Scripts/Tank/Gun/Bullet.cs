using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float  _bulletSpeed = 50f;
    [SerializeField] private float _maxLiveTime = 5f;
    [SerializeField] private float _damageAmount = 100;
    [SerializeField] private float _damageRandomRange = 10;
    [SerializeField] private GameObject _vfxPoint;
    [SerializeField] private GameObject _regularExplosionVFX;
    [SerializeField] private GameObject _groundExplosionVFX;
    [SerializeField] private string _explosionSound;
    private Rigidbody _rigidbody;

    private void Start()
    {
        StartCoroutine(DestroyOnMiss());
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        _rigidbody.MovePosition(transform.position + transform.forward * _bulletSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.isTrigger)
        {
            return;
        }

        if(string.IsNullOrEmpty(_explosionSound) == false)
        {
            AudioHandler.PlayGameSound(_explosionSound, gameObject);
        }

        HitTarget(other.gameObject);
        GenerateFX(other.tag);
        Destroy(gameObject);
    }

    private IEnumerator DestroyOnMiss()
    {
        yield return new WaitForSeconds(_maxLiveTime);
        Destroy(gameObject);
    }

    private void GenerateFX(string gameObjectTag)
    {
        GameObject fxType = gameObjectTag == GameTags.Ground ? _groundExplosionVFX : _regularExplosionVFX;
        Instantiate(fxType, _vfxPoint.transform.position, transform.rotation);
    }

    private void HitTarget(GameObject target)
    {
        ArmorPart armorPart;
        if (target.TryGetComponent(out armorPart))
        {
            float damage = Random.Range(_damageAmount - _damageRandomRange, _damageAmount + _damageRandomRange);
            armorPart.TakeDamage(damage);
        }
    }
}
