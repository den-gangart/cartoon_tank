using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class MixerGroupComponent
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private string _parameterName;
    [SerializeField, Range(0.001f, 1)] private float _volume;

    public void UpdateVolume(float volume)
    {
        _volume = volume;
        Synchronize();
    }

    public void Synchronize()
    {
        _audioMixer.SetFloat(_parameterName, Mathf.Log(_volume) * 20);
    }

    public float GetVolume()
    {
        return _volume;
    }
}
