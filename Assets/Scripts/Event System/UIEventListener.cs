using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEventListener : MonoBehaviour
{
    private void Awake()
    {
        EventSystem.AddEventListener(EContentEventType.GamePause, OnShowUI);
        EventSystem.AddEventListener(EContentEventType.GameResume, OnGameResumed);
        EventSystem.AddEventListener(EContentEventType.Win, OnShowUI);
        EventSystem.AddEventListener(EContentEventType.Lose, OnShowUI);
    }

    private void OnDestroy()
    {
        EventSystem.RemoveEventListener(EContentEventType.GamePause, OnShowUI);
        EventSystem.RemoveEventListener(EContentEventType.GameResume, OnGameResumed);
        EventSystem.RemoveEventListener(EContentEventType.Win, OnShowUI);
        EventSystem.RemoveEventListener(EContentEventType.Lose, OnShowUI);
    }

    protected virtual void OnGameResumed() {}
    protected virtual void OnShowUI() { }
}
