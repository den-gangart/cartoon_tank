using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum SoundSequenceType
{
    Successively,
    Random,
    RandomOnce,
}

[System.Serializable]
public class SceneBackgroundSoundSettings
{
    [SerializeField] private string _sceneName;
    [SerializeField] private List<string> _soundList;
    [SerializeField] private SoundSequenceType _soundSequenceType;

    public string GetName()
    {
        return _sceneName;
    }

    public string GetSound(int index)
    {
        return _soundList[index];
    }

    public SoundSequenceType GetSoundSequenceType()
    {
        return _soundSequenceType;
    }

    public int GetSoundCount()
    {
        return _soundList.Count;
    }
}