using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventBus
{
    private static readonly IDictionary<GameEventType, UnityEvent> events = new Dictionary<GameEventType, UnityEvent>();

    public static void RegisterEvent(GameEventType gameEventType, UnityAction action)
    {
        UnityEvent thisEvent;

        if (events.TryGetValue(gameEventType, out thisEvent))
        {
            thisEvent.AddListener(action);
        }
        else
        {
            thisEvent = new UnityEvent();
            events.Add(gameEventType, thisEvent);
            thisEvent.AddListener(action);
        }
    }

    public static void UnregisterEvent(GameEventType gameEventType, UnityAction action)
    {
        UnityEvent thisEvent;

        if (events.TryGetValue(gameEventType, out thisEvent))
        {
            thisEvent.RemoveListener(action);
        }
    }

    public static void Publish(GameEventType gameEventType)
    {
        UnityEvent thisEvent;

        if (events.TryGetValue(gameEventType, out thisEvent))
        {
            thisEvent?.Invoke();
        }
    }
}
