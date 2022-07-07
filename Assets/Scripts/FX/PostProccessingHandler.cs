using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[RequireComponent(typeof(PostProcessVolume))]
public class PostProccessingHandler : MonoBehaviour
{
    [SerializeField] private float _defaultSaturation = 0;
    [SerializeField] private float _finishSaturation = 0;

    [SerializeField] private float _defaultBloomIntensity = 0;
    [SerializeField] private float _finishBloomIntensity = 30;

    private PostProcessVolume _postProcessVolume;
    private ColorGrading _colorGrading;
    private Bloom _bloom;

    private void Awake()
    {
        _postProcessVolume = GetComponent<PostProcessVolume>();
        _postProcessVolume.profile.TryGetSettings(out _colorGrading);
        _postProcessVolume.profile.TryGetSettings(out _bloom);

        EventSystem.AddEventListener(EContentEventType.Win, OnLevelFinished);
        EventSystem.AddEventListener(EContentEventType.Lose, OnLevelFinished);

        _colorGrading.saturation.value = _defaultSaturation;
        _bloom.intensity.value = _defaultBloomIntensity;
    }

    private void OnLevelFinished()
    {
        _colorGrading.saturation.value = _finishSaturation;
        _bloom.intensity.value = _finishBloomIntensity;
    }

    private void OnDestroy()
    {
        EventSystem.RemoveEventListener(EContentEventType.Win, OnLevelFinished);
        EventSystem.RemoveEventListener(EContentEventType.Lose, OnLevelFinished);
    }
}
