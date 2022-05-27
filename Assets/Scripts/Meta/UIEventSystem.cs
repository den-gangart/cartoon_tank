using System;
using System.Collections.Generic;
using UnityEngine;

public enum EUIEvent
{
   HealthChanged,
   Win,
   Lose,
}

public class UIEventSystem: MonoBehaviour
{
    public static UIEventSystem Instance { get; private set; }
    private Dictionary<EUIEvent, Action> _UIEventsListeners = new Dictionary<EUIEvent, Action>();
    private Dictionary<EUIEvent, Action<object>> _UIEventsListenersWithParamater = new Dictionary<EUIEvent, Action<object>>();

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public static void AddUIEventListener(EUIEvent wheelEvent, Action evt)
    {
        if (Instance._UIEventsListeners.TryGetValue(wheelEvent, out Action action))
        {
            Instance._UIEventsListeners[wheelEvent] = action += evt;
        }
        else
        {
            Instance._UIEventsListeners[wheelEvent] = evt;
        }
    }

    public static void AddUIEventListener<T>(EUIEvent wheelEvent, Action<T> evt)
    {
        Action<object> newAction = (e) => evt((T)e);

        if (Instance._UIEventsListenersWithParamater.TryGetValue(wheelEvent, out Action<object> action))
        {
            Instance._UIEventsListenersWithParamater[wheelEvent] = action += newAction;
        }
        else
        {
            Instance._UIEventsListenersWithParamater[wheelEvent] = newAction;
        }
    }

    public static void RemoveUIEventListener(EUIEvent wheelEvent, Action evt)
    {
        if (Instance._UIEventsListeners.TryGetValue(wheelEvent, out var action))
        {
            action -= evt;
            if (action == null)
            {
                Instance._UIEventsListeners.Remove(wheelEvent);
            }
            else
            {
                Instance._UIEventsListeners[wheelEvent] = action;
            }
        }
    }

    public static void RemoveUIEventListener<T>(EUIEvent wheelEvent, Action<T> evt)
    {
        if (Instance._UIEventsListenersWithParamater.TryGetValue(wheelEvent, out var action))
        {
            Action<object> removeAction = (e) => evt((T)e);
            action -= removeAction;

            if (action == null)
            {
                Instance._UIEventsListenersWithParamater.Remove(wheelEvent);
            }
            else
            {
                Instance._UIEventsListenersWithParamater[wheelEvent] = action;
            }
        }
    }

    public static void Broadcast(EUIEvent wheelEvent)
    {
        if (Instance._UIEventsListeners.TryGetValue(wheelEvent, out var action))
        {
            action.Invoke();
        }
    }

    public static void Broadcast(EUIEvent wheelEvent, object obj)
    {
        if (Instance._UIEventsListenersWithParamater.TryGetValue(wheelEvent, out var action))
        {
            action.Invoke(obj);
        }
    }

    private void OnDestroy()
    {
        if(Instance == this)
        {
            Instance = null;
        }
    }
}
