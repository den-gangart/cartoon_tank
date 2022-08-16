using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsHandler : MonoBehaviour
{
    [SerializeField] private Button _saveButton;
    [SerializeField] private Slider _senivitySlider;
    [SerializeField] private float  _sensivityDefaultValue;
    [SerializeField] private List<Slider> _audioSliders;
    [SerializeField] private AudioSettings _audioSettings;

    private float _mouseSensivity;
    private List<float> _audioAmountList;

    public const string SENSIVITY_KEY = "MouseSens";

    private void Start()
    {
        _saveButton.onClick.AddListener(OnButtonSaveClicked);
        _audioAmountList = new List<float>(new float[_audioSliders.Count]);

        LoadBaseValues();
    }
    
    private void OnButtonSaveClicked()
    {
        _mouseSensivity = _senivitySlider.value;
        PlayerPrefs.SetFloat(SENSIVITY_KEY, _mouseSensivity);

        for (int i = 0; i < _audioSliders.Count; i++)
        {
            _audioAmountList[i] = _audioSliders[i].value;
        }

        _audioSettings.UpdateMixerVolume(_audioAmountList);
        EventSystem.Broadcast(EContentEventType.SettingsUpdated);
    }

    private void LoadBaseValues()
    {
        _senivitySlider.value = PlayerPrefs.GetFloat(SENSIVITY_KEY, _sensivityDefaultValue);

        for(int i = 0; i < _audioSliders.Count; i++)
        {
            _audioSliders[i].value = _audioSettings.GetMixerParameterAmount(i);
        }
    }
}
