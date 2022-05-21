using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootReaction : MonoBehaviour
{
    [SerializeField] private GunShooter _gunShooter;
    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private float _impulsePower = 1000f;

    private void Start()
    {
        if(_gunShooter != null)
        {
            _gunShooter.AddShootListener(OnShoot);
        }
    }

    private void OnShoot(Vector3 forward)
    {
        _rigidBody.AddForce(forward * - 1 * _impulsePower, ForceMode.Impulse);
    }
}
