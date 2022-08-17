using UnityEngine;

[RequireComponent(typeof(Health))]
public class HealthBarListener : MonoBehaviour
{
    [SerializeField] private EnemyHealthBar _enemyHealthBar;

    private Health _health;
    private bool _isBarShowed;

    private void Start()
    {
        _health = GetComponent<Health>();
        _health.HealthChange += OnHealthChanged;
        _health.Killed += OnKilled;
    }

    private void OnHealthChanged(object sender, float _healthAmount)
    {
        if(_isBarShowed == false)
        {
            _enemyHealthBar.Show();
            _isBarShowed = true;
        }

        _enemyHealthBar.UpdateHealthAmount(_health.GetHealthInPercent());
    }

    private void OnKilled()
    {
        _enemyHealthBar.Hide();
    }

    private void OnDestroy()
    {
        _health.Killed -= OnKilled;
        _health.HealthChange -= OnHealthChanged;
    }
}
