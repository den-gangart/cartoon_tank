using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : HealthBar
{
    private Transform _cameraTransfrom;

    private void Start()
    {
        _cameraTransfrom = Camera.main.transform;
        Hide();
    }

    private void Update()
    {
        if (_isActive)
        {
            transform.LookAt(_cameraTransfrom);
        }
    }
}
