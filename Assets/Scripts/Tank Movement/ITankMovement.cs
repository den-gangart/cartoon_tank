using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITankMovement
{
    BodyMovement Body { get; } 
    TowerMovement Tower { get; }
    GunMovement Gun { get;  }

    void Move(float horizontalInput, float verticalInput, Quaternion rotationTarget);
}
