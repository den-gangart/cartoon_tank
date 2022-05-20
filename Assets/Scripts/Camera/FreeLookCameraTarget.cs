using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CinemachineFreeLook))] 
public class FreeLookCameraTarget : MonoBehaviour
{
    [SerializeField] private string _targetTag = "Player";
    private CinemachineFreeLook _cinemachineFreeLook;

    void Awake()
    {
        _cinemachineFreeLook = GetComponent<CinemachineFreeLook>();
        GameObject target = GameObject.FindGameObjectWithTag(_targetTag);

        if(target == null)
        {
            Debug.LogError(GameErrorMessages.MISSING_CINEMACHINE_TARGET);
        }
        else
        {
            _cinemachineFreeLook.Follow = target.transform;
            _cinemachineFreeLook.LookAt = target.transform;
        }
    }
}
