using UnityEngine;

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
