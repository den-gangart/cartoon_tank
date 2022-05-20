using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerMovement : MonoBehaviour
{
    [SerializeField] private float _turnSpeed;
    [SerializeField] private Transform _towerTransform;


   public void Rotate(Quaternion rotationTarget) 
   {
        float _targetDRotationAnlge = rotationTarget.eulerAngles.y - transform.eulerAngles.y;
        Quaternion target = Quaternion.Euler(0, _targetDRotationAnlge, 0);
        _towerTransform.localRotation = Quaternion.Lerp(_towerTransform.localRotation, target, Time.deltaTime * _turnSpeed);
   }
}
