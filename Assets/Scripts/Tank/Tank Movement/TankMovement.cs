using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BodyMovement))]
[RequireComponent(typeof(TowerMovement))]
[RequireComponent(typeof(GunMovement))]
public class TankMovement : MonoBehaviour, ITankMovement
{
    public BodyMovement Body { get; private set; }
    public TowerMovement Tower { get; private set; }
    public GunMovement Gun { get; private set; }

    private void Start()
    {
        Body = GetComponent<BodyMovement>();
        Tower = GetComponent<TowerMovement>();
        Gun = GetComponent<GunMovement>();
    }

    public void Move(float horizontalInput, float verticalInput, Quaternion rotationTarget)
    {
        Body.Move(horizontalInput, verticalInput);
        Tower.Rotate(rotationTarget);
        Gun.Rotate(rotationTarget);
    }
}
