using System;
using System.Collections.Generic;
using System.Linq;

public enum EContentEventType
{
    HealthChanged,
    Win,
    Lose,
    PlayerDead,
    GamePause,
    GameResume,
    TaskCompleted,
    TaskAdded,
    CaptureAdded,
    StopBackgroundMusic,
    PlayBackgroundMusic,
    PauseButtonPressed,
}

public static class EventSystem
{
    private static Dictionary<EContentEventType, Delegate> _eventListeners = new Dictionary<EContentEventType, Delegate>();
    private static Dictionary<EContentEventType, Delegate> _eventListenersWithParamater = new Dictionary<EContentEventType, Delegate>();

    public static void AddEventListener(EContentEventType eventType, Action callBack)
    {
        if (_eventListeners.TryGetValue(eventType, out Delegate mergedCallBacks))
        {
            _eventListeners[eventType] = Delegate.Combine(mergedCallBacks, callBack);
        }
        else
        {
            _eventListeners[eventType] = callBack;
        }
    }

    public static void AddEventListener<T>(EContentEventType eventType, Action<T> callBack)
    {
        if (_eventListenersWithParamater.TryGetValue(eventType, out Delegate mergedCallBacks))
        {
            _eventListenersWithParamater[eventType] = Delegate.Combine(mergedCallBacks, callBack);
        }
        else
        {
            _eventListenersWithParamater[eventType] = callBack;
        }
    }

    public static void RemoveEventListener(EContentEventType eventType, Action callBack)
    {
        if (_eventListeners.TryGetValue(eventType, out Delegate mergedCallBacks))
        {
            mergedCallBacks = Delegate.Remove(mergedCallBacks, callBack);
            if (mergedCallBacks == null)
            {
                _eventListeners.Remove(eventType);
            }
            else
            {
                _eventListeners[eventType] = mergedCallBacks;
            }
        }
    }

    public static void RemoveEventListener<T>(EContentEventType eventType, Action<T> callBack)
    {
        if (_eventListenersWithParamater.TryGetValue(eventType, out Delegate mergedCallBacks))
        { 
            mergedCallBacks = Delegate.Remove(mergedCallBacks, callBack);
            if (mergedCallBacks == null)
            {
                _eventListenersWithParamater.Remove(eventType);
            }
            else
            {
                _eventListenersWithParamater[eventType] = mergedCallBacks;
            }
        }
    }

    public static void Broadcast(EContentEventType eventType)
    {
        if (_eventListeners.TryGetValue(eventType, out var mergedCallBacks))
        {
            Action[] actionList = mergedCallBacks.GetInvocationList().Cast<Action>().ToArray();
            foreach(Action action in actionList)
            {
                action.Invoke();
            }
        }
    }

    public static void Broadcast<T>(EContentEventType eventType, T arg)
    {
        if (_eventListenersWithParamater.TryGetValue(eventType, out var mergedCallBacks))
        {
            Action<T>[] actionList = mergedCallBacks.GetInvocationList().Cast<Action<T>>().ToArray();
            foreach (Action<T> action in actionList)
            {
                action.Invoke(arg);
            }
        }
    }
}
