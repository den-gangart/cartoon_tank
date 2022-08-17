using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineFreeLook))]
public class CMContentEventListener : UIEventListener
{
    private CinemachineFreeLook _cinemachineFreeLook;

    private void Start()
    {
        _cinemachineFreeLook = GetComponent<CinemachineFreeLook>();
    }

    protected override void OnGameResumed()
    {
        _cinemachineFreeLook.enabled = true;
    }

    protected override void OnShowUI()
    {
        _cinemachineFreeLook.enabled = false;
    }
}
