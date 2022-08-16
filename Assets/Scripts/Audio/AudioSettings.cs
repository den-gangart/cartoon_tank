using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "Audio Settings", menuName = "Scriptable Objects/AudioSettings")]
public class AudioSettings : ScriptableObject
{
    [SerializeField] private List<AudioComponent> _audioList;
    [SerializeField] private List<MixerGroupComponent> _mixerGroupList;

    public void SynchronizeMixerGroups()
    {
        foreach (MixerGroupComponent _mixerComponent in _mixerGroupList)
        {
            _mixerComponent.Synchronize();
        }
    }

    public Dictionary<string, AudioComponent> GetAudioDictionary()
    {
        Dictionary<string, AudioComponent> _audioDictionary = new Dictionary<string, AudioComponent>();

        foreach (AudioComponent audioComponent in _audioList)
        {
            _audioDictionary.Add(audioComponent.GetAssetName(), audioComponent);
        }

        return _audioDictionary;
    }

    public void UpdateMixerVolume(List<float> volumeAmount)
    {
        for (int i = 0; i < volumeAmount.Count && i < _mixerGroupList.Count; i++)
        {
            _mixerGroupList[i].UpdateVolume(volumeAmount[i]);
        }
    }

    public float GetMixerParameterAmount(int index)
    {
        return _mixerGroupList[index].GetVolume();
    }
}
