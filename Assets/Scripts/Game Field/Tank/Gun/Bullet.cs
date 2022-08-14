using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float  _bulletSpeed = 50f;
    [SerializeField] private float _maxLiveTime = 5f;
    [SerializeField] private float _damageAmount = 100;
    [SerializeField] private float _damageRandomRange = 10;
    [SerializeField] private GameObject _vfxPoint;
    [SerializeField] private GameObject _regularExplosionVFX;
    [SerializeField] private GameObject _groundExplosionVFX;

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
        if(other.isTrigger)
        {
            return;
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
