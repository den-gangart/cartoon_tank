using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class AudioComponent
{
    [Header("Main Info")]
    [SerializeField] private string _assetName;
    [SerializeField] private AudioClip _clip;
    [SerializeField] private AudioMixerGroup _mixerGroup;

    [Header("Parameters")]
    [SerializeField, Range(0, 1)] private float _volume = 1f;
    [SerializeField] private bool _loop = false;
    [SerializeField, Range(-3, 3)] private float _pitch = 0f;
    [SerializeField, Range(-1, 1)] private float _stereoPan = 0f;
    [SerializeField, Range(0, 1)] private float _spatialBlend = 0f;
    [SerializeField, Range(0, 1.1f)] private float _reverbZoneMix = 1f;

    public void ApplySettingsToSource(AudioSource audioSource)
    {
        audioSource.clip = _clip;
        audioSource.outputAudioMixerGroup = _mixerGroup;
        audioSource.volume = _volume;
        audioSource.loop = _loop;
        audioSource.pitch = _pitch;
        audioSource.panStereo = _stereoPan;
        audioSource.spatialBlend = _spatialBlend;
        audioSource.reverbZoneMix = _reverbZoneMix;
    }

    public string GetAssetName()
    {
        return _assetName;
    }
}
