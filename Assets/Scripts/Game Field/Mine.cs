using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField] private GameObject _explosionVFX;

    private void OnTriggerEnter(Collider other)
    {
        ArmorPart armor;
        if(!other.TryGetComponent(out armor))
        {
            return;
        }

        armor.ForceKill();
        Instantiate(_explosionVFX, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
