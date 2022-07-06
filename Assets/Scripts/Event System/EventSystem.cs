using System;
using System.Collections.Generic;
using UnityEngine;

public enum EContentEventType
{
    HealthChanged,
    Win,
    Lose,
    PlayerDead,
    GamePause,
    GameResume,
    TaskCompleted,
}

public class EventSystem: Singleton<EventSystem>
{
    private Dictionary<EContentEventType, Action> _eventListeners = new Dictionary<EContentEventType, Action>();
    private Dictionary<EContentEventType, Action<object>> _eventListenersWithParamater = new Dictionary<EContentEventType, Action<object>>();

    public static void AddEventListener(EContentEventType eventType, Action evt)
    {
        if (Instance._eventListeners.TryGetValue(eventType, out Action action))
        {
            Instance._eventListeners[eventType] = action += evt;
        }
        else
        {
            Instance._eventListeners[eventType] = evt;
        }
    }

    public static void AddEventListener<T>(EContentEventType eventType, Action<T> evt)
    {
        Action<object> newAction = (e) => evt((T)e);

        if (Instance._eventListenersWithParamater.TryGetValue(eventType, out Action<object> action))
        {
            Instance._eventListenersWithParamater[eventType] = action += newAction;
        }
        else
        {
            Instance._eventListenersWithParamater[eventType] = newAction;
        }
    }

    public static void RemoveEventListener(EContentEventType eventType, Action evt)
    {
        if (Instance._eventListeners.TryGetValue(eventType, out var action))
        {
            action -= evt;
            if (action == null)
            {
                Instance._eventListeners.Remove(eventType);
            }
            else
            {
                Instance._eventListeners[eventType] = action;
            }
        }
    }

    public static void RemoveEventListener<T>(EContentEventType eventType, Action<T> evt)
    {
        if (Instance._eventListenersWithParamater.TryGetValue(eventType, out var action))
        {
            Action<object> removeAction = (e) => evt((T)e);
            action -= removeAction;

            if (action == null)
            {
                Instance._eventListenersWithParamater.Remove(eventType);
            }
            else
            {
                Instance._eventListenersWithParamater[eventType] = action;
            }
        }
    }

    public static void Broadcast(EContentEventType eventType)
    {
        if (Instance._eventListeners.TryGetValue(eventType, out var action))
        {
            action.Invoke();
        }
    }

    public static void Broadcast(EContentEventType eventType, object obj)
    {
        if (Instance._eventListenersWithParamater.TryGetValue(eventType, out var action))
        {
            action.Invoke(obj);
        }
    }
}
