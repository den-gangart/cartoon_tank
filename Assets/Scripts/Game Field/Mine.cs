using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField] private GameObject _explosionVFX;
    [SerializeField] private string _explosionSound;

    private void OnTriggerEnter(Collider other)
    {
        ArmorPart armor;
        if(!other.TryGetComponent(out armor))
        {
            return;
        }

        armor.ForceKill();
        Instantiate(_explosionVFX, transform.position, transform.rotation);

        if (string.IsNullOrEmpty(_explosionSound) == false)
        {
            AudioHandler.PlayGameSound(_explosionSound, gameObject);
        }

        Destroy(gameObject);
    }
}
