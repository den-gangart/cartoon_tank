using Cinemachine;
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

        _cinemachineFreeLook.Follow = target.transform;
        _cinemachineFreeLook.LookAt = target.transform;
    }
}
