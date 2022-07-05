using System;
using System.Collections.Generic;
using UnityEngine;

public enum EUIEvent
{
   HealthChanged,
   Win,
   Lose,
   GamePause,
   GameResume,
}

public class UIEventSystem: Singleton<UIEventSystem>
{
    private Dictionary<EUIEvent, Action> _UIEventsListeners = new Dictionary<EUIEvent, Action>();
    private Dictionary<EUIEvent, Action<object>> _UIEventsListenersWithParamater = new Dictionary<EUIEvent, Action<object>>();

    public static void AddUIEventListener(EUIEvent uiEvent, Action evt)
    {
        if (Instance._UIEventsListeners.TryGetValue(uiEvent, out Action action))
        {
            Instance._UIEventsListeners[uiEvent] = action += evt;
        }
        else
        {
            Instance._UIEventsListeners[uiEvent] = evt;
        }
    }

    public static void AddUIEventListener<T>(EUIEvent uiEvent, Action<T> evt)
    {
        Action<object> newAction = (e) => evt((T)e);

        if (Instance._UIEventsListenersWithParamater.TryGetValue(uiEvent, out Action<object> action))
        {
            Instance._UIEventsListenersWithParamater[uiEvent] = action += newAction;
        }
        else
        {
            Instance._UIEventsListenersWithParamater[uiEvent] = newAction;
        }
    }

    public static void RemoveUIEventListener(EUIEvent uiEvent, Action evt)
    {
        if (Instance._UIEventsListeners.TryGetValue(uiEvent, out var action))
        {
            action -= evt;
            if (action == null)
            {
                Instance._UIEventsListeners.Remove(uiEvent);
            }
            else
            {
                Instance._UIEventsListeners[uiEvent] = action;
            }
        }
    }

    public static void RemoveUIEventListener<T>(EUIEvent uiEvent, Action<T> evt)
    {
        if (Instance._UIEventsListenersWithParamater.TryGetValue(uiEvent, out var action))
        {
            Action<object> removeAction = (e) => evt((T)e);
            action -= removeAction;

            if (action == null)
            {
                Instance._UIEventsListenersWithParamater.Remove(uiEvent);
            }
            else
            {
                Instance._UIEventsListenersWithParamater[uiEvent] = action;
            }
        }
    }

    public static void Broadcast(EUIEvent uiEvent)
    {
        if (Instance._UIEventsListeners.TryGetValue(uiEvent, out var action))
        {
            action.Invoke();
        }
    }

    public static void Broadcast(EUIEvent uiEvent, object obj)
    {
        if (Instance._UIEventsListenersWithParamater.TryGetValue(uiEvent, out var action))
        {
            action.Invoke(obj);
        }
    }
}
