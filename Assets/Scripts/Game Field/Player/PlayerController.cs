using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private TankMovement _tankMovement;
    [SerializeField] private GunShooter _gunShooter;

    [SerializeField] private Camera _camera;
    private Quaternion _cameraRotation => _camera.transform.rotation;

    private bool _controlEnabled = true;

    private void Start()
    {
        if (_tankMovement == null)
        {
            Debug.LogError(GameErrorMessages.MISSING_TANK_MOVEMENT);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && _controlEnabled)
        {
            _gunShooter.TryShoot();
        }
    }

    private void FixedUpdate()
    {
        if(_controlEnabled == false)
        {
            return;
        }

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        _tankMovement.Move(horizontalInput, verticalInput, _cameraRotation);
    }

    public void EnableControl()
    {
        _controlEnabled = true;
    }

    public void DisableControl()
    {
        _controlEnabled = false;
    }
}
