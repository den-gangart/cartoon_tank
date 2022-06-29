using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEventDispatcher : MonoBehaviour
{
    [SerializeField] private Health _health; 

    private void Start()
    {
        if (_health != null)
        {
            _health.HealthChange += OnHealthChanged;
        }
    }

    private void OnHealthChanged(object sender, float healthAmount)
    {
        UIEventSystem.Broadcast(EUIEvent.HealthChanged, healthAmount);
    }

    private void OnDestroy()
    {
        if (_health != null)
        {
            _health.HealthChange -= OnHealthChanged;
        }
    }
}
