using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunMovement : MonoBehaviour
{
    [SerializeField] private float _maxAngleDown = 20;
    [SerializeField] private float _angleOffset = 20;
    [SerializeField] private float _turnSpeed;
    [SerializeField] private Transform _gunTransform;
    private float _lowerPosition = 100;

    public void Rotate(Quaternion rotationTarget)
    {
        float targetRotationAnlge = ClampAngle(rotationTarget.eulerAngles.x + _angleOffset);
        Quaternion target = Quaternion.Euler(targetRotationAnlge, 0, 0);
        _gunTransform.localRotation = Quaternion.Lerp(_gunTransform.localRotation, target, Time.deltaTime * _turnSpeed);
    }

    private float ClampAngle(float targetRotationAnlge)
    {
        if (targetRotationAnlge < _lowerPosition && targetRotationAnlge > _maxAngleDown)
        {
            return _maxAngleDown;
        }

        return targetRotationAnlge;
    }
}
