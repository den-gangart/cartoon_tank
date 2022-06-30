using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyMovement : MonoBehaviour
{
    [SerializeField] private List<SuspensionInfo> _suspensionInfos;
    [SerializeField] private float _maxMotorTorque;
    [SerializeField] private float _maxSteeringAngle;

    public void Move(float horizontalInput, float verticalInput)
    {
        float motor = _maxMotorTorque * verticalInput;
        float steering = _maxSteeringAngle * horizontalInput;

        for (int index = 0; index < _suspensionInfos.Count; index++)
        {
            SuspensionInfo suspensionInfo = _suspensionInfos[index];
            WheelCollider wheelCollider = suspensionInfo.wheelCollider;

            switch (suspensionInfo.wheelType)
            {
                case WheelType.Motor:
                    wheelCollider.motorTorque = motor;
                    break;
                case WheelType.Steering:
                    wheelCollider.steerAngle = steering;
                    break;
                case WheelType.MotorAndSteering:
                    wheelCollider.motorTorque = motor;
                    wheelCollider.steerAngle = steering;
                    break;
                default:
                    break;
            }

            ApplyMovementToWheelModel(wheelCollider, suspensionInfo.wheelModel);
        }
    }

    private void ApplyMovementToWheelModel(WheelCollider wheelCollider, GameObject wheelModel)
    {
        Vector3 position;
        Quaternion rotation;
        wheelCollider.GetWorldPose(out position, out rotation);

        wheelModel.transform.position = position;
        wheelModel.transform.rotation = rotation;
    }
}

public enum WheelType
{
    Regular,
    Motor,
    Steering,
    MotorAndSteering,
}

[System.Serializable]
public class SuspensionInfo
{
    public WheelCollider wheelCollider;
    public GameObject wheelModel;
    public WheelType wheelType;
}
