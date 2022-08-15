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

    [SerializeField] private float _defaultFocusDistance = 7.67f;
    [SerializeField] private float _finishFocusDistance = 0.1f;

    private PostProcessVolume _postProcessVolume;
    private ColorGrading _colorGrading;
    private Bloom _bloom;
    private DepthOfField _depthOfField;

    private void Awake()
    {
        _postProcessVolume = GetComponent<PostProcessVolume>();
        _postProcessVolume.profile.TryGetSettings(out _colorGrading);
        _postProcessVolume.profile.TryGetSettings(out _bloom);
        _postProcessVolume.profile.TryGetSettings(out _depthOfField);

        EventSystem.AddEventListener(EContentEventType.Win, OnLevelFinished);
        EventSystem.AddEventListener(EContentEventType.Lose, OnLevelFinished);
        EventSystem.AddEventListener(EContentEventType.GamePause, OnLevelPause);
        EventSystem.AddEventListener(EContentEventType.GameResume, OnLevelResume);

        SetDefaultValues();
    }

    private void OnLevelFinished()
    {
        SetFinishValues();
    }

    private void OnLevelPause()
    {
        _depthOfField.focusDistance.value = _finishFocusDistance;
        SetFinishValues();
    }

    private void OnLevelResume()
    {
        _depthOfField.focusDistance.value = _defaultFocusDistance;
        SetDefaultValues();
    }

    private void SetFinishValues()
    {
        _colorGrading.saturation.value = _finishSaturation;
        _bloom.intensity.value = _finishBloomIntensity;
    }

    private void SetDefaultValues()
    {
        _colorGrading.saturation.value = _defaultSaturation;
        _bloom.intensity.value = _defaultBloomIntensity;
    }

    private void OnDestroy()
    {
        EventSystem.RemoveEventListener(EContentEventType.Win, OnLevelFinished);
        EventSystem.RemoveEventListener(EContentEventType.Lose, OnLevelFinished);
        EventSystem.RemoveEventListener(EContentEventType.GamePause, OnLevelPause);
        EventSystem.RemoveEventListener(EContentEventType.GameResume, OnLevelResume);
    }
}
