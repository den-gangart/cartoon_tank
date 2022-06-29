using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovement : MonoBehaviour, ITankMovement
{
    [SerializeField] private bool _canMove;
    [SerializeField] private BodyMovement _bodyMovement;
    [SerializeField] private TowerMovement _towerMovement;
    [SerializeField] private GunMovement _gunMovement;

    public BodyMovement Body { get {return _bodyMovement; } }
    public TowerMovement Tower { get { return _towerMovement; } }
    public GunMovement Gun { get { return _gunMovement; } }

    public void Move(float horizontalInput, float verticalInput, Quaternion rotationTarget)
    {
       if(_canMove == false)
       {
           return;
       }

       Body.Move(horizontalInput, verticalInput);
       Tower.Rotate(transform, rotationTarget);
       Gun.Rotate(rotationTarget);
    }

    public void DisableMovement()
    {
        _canMove = false;
        Body.Move(0, 0);
    }

    public void EnableMovement()
    {
        _canMove = true;
    }
}
