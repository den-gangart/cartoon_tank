using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] protected GameObject _anchor;
    [SerializeField] protected Image _healthBar;
    [SerializeField] protected Gradient _healthGradient;

    protected bool _isActive = false;

    public void UpdateHealthAmount(float _healthAmount)
    {
        _healthBar.fillAmount = _healthAmount;
        _healthBar.color = _healthGradient.Evaluate(_healthAmount);
    }

    public void Show()
    {
        _isActive = true;
        _anchor.SetActive(true);
    }

    public void Hide()
    {
        _isActive = false;
        _anchor.SetActive(false);
    }
}
