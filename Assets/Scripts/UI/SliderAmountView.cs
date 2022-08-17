using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Slider))]
public class SliderAmountView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshPro;
    private Slider _slider;

    private void Start()
    {
        _slider = GetComponent<Slider>();
        _slider.onValueChanged.AddListener(OnValueChanged);
        SetupValue(_slider.value);
    }

    private void OnValueChanged(float value)
    {
        SetupValue(value);
    }

    private void OnDestroy()
    {
        _slider.onValueChanged.RemoveListener(OnValueChanged);
    }

    private void SetupValue(float value)
    {
        _textMeshPro.text = value.ToString("0.00");
    }
}
