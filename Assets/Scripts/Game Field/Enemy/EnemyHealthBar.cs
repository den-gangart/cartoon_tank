using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private GameObject _anchor;
    [SerializeField] private Image _healthBar;
    [SerializeField] private Gradient _healthGradient;
    
    private Transform _cameraTransfrom;
    private bool _isActive = false;

    private void Start()
    {
        _cameraTransfrom = Camera.main.transform;
        Hide();
    }

    private void Update()
    {
        if (_isActive)
        {
            transform.LookAt(_cameraTransfrom);
        }
    }

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
