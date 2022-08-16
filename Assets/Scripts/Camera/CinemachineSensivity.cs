using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CinemachineFreeLook))]
public class CinemachineSensivity : MonoBehaviour
{
    [SerializeField] private float _xMaxSpeed = 300f;
    [SerializeField] private float _yMaxSpeed = 10f;

    private CinemachineFreeLook _cinemachineFreeLook;
    private float _sensivity = 1f;

    private void Awake()
    {
        _cinemachineFreeLook = GetComponent<CinemachineFreeLook>();
        EventSystem.AddEventListener(EContentEventType.SettingsUpdated, OnSettingsUpdated);
        SetSinemachineSensivity();
    }

    private void OnSettingsUpdated()
    {
        SetSinemachineSensivity();
    }
    
    private void SetSinemachineSensivity()
    {
        _sensivity = PlayerPrefs.GetFloat(SettingsHandler.SENSIVITY_KEY, 1f);
        _cinemachineFreeLook.m_XAxis.m_MaxSpeed = _xMaxSpeed * _sensivity;
        _cinemachineFreeLook.m_YAxis.m_MaxSpeed = _yMaxSpeed * _sensivity;
    }

    private void OnDestroy()
    {
        EventSystem.RemoveEventListener(EContentEventType.SettingsUpdated, OnSettingsUpdated);
    }
}
