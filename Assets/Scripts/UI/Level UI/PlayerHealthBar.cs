public class PlayerHealthBar : HealthBar
{
    private float _baseValue = 1f;

    private void Awake()
    {
        EventSystem.AddEventListener<float>(EContentEventType.HealthChanged, UpdateHealthAmount);
        EventSystem.AddEventListener(EContentEventType.Win, Hide);
        EventSystem.AddEventListener(EContentEventType.Lose, Hide);

        UpdateHealthAmount(_baseValue);
        Show();
    }

    private void OnDestroy()
    {
        EventSystem.RemoveEventListener<float>(EContentEventType.HealthChanged, UpdateHealthAmount);
        EventSystem.RemoveEventListener(EContentEventType.Win, Hide);
        EventSystem.RemoveEventListener(EContentEventType.Lose, Hide);
    }
}
