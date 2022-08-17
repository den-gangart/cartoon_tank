using UnityEngine;

public class PlayerEventDispatcher : MonoBehaviour
{
    [SerializeField] private Health _health; 

    private void Start()
    {
        if (_health != null)
        {
            _health.HealthChange += OnHealthChanged;
            _health.Killed += OnPlayerDead;
        }
    }

    private void OnDestroy()
    {
        if (_health != null)
        {
            _health.HealthChange -= OnHealthChanged;
            _health.Killed -= OnPlayerDead;
        }
    }

    private void OnHealthChanged(object sender, float healthAmount)
    {
        EventSystem.Broadcast(EContentEventType.HealthChanged, _health.GetHealthInPercent());
    }

    private void OnPlayerDead()
    {
        EventSystem.Broadcast(EContentEventType.PlayerDead);
    }

}
