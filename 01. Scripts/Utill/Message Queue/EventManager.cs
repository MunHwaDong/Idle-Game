using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EventManager : Singleton<EventManager>
{
    public void PublishEventQueue(string eventName)
    {
        if (_eventQueues.TryGetValue(eventName, out var q))
        {
            while (q.Count > 0)
            {
                EventMessage msg = q.Dequeue();
                
                ProcessEvent(msg);
            }
        }
        else
        {
            throw new Exception("해당 Event를 찾을 수 없습니다.");
        }
    }

    private void ProcessEvent(EventMessage eventMessage)
    {
        if (_listeners.TryGetValue(eventMessage.EventName, out var listener))
        {
            foreach (var (evt, listenerObj) in listener.ToList())
            {
                if (listener is null)
                    throw new Exception(listenerObj.name + "'s IOnEventSO is null!!!");
                //같은 이벤트를 기다리는 게임 오브젝트들이 여러개 일 때 값(게임 오브젝트)를 덮어 쓴다.
                eventMessage.AddParameter<GameObject>(listenerObj);
                
                evt.OnEvent(eventMessage);
            }
        }
    }

    public void PushEventMessage(EventMessage eventMessage)
    {
        string eventName = eventMessage.EventName;

        if (_eventQueues.TryGetValue(eventName, out var q))
        {
            q.Enqueue(eventMessage);
        }
        else
        {
            var eventQueue = new Queue<EventMessage>();
            eventQueue.Enqueue(eventMessage);
            _eventQueues.Add(eventName, eventQueue);
        }
    }

    public void AddListener(string eventName, IOnEvent listener, GameObject listenerObj = null)
    {
        if (_listeners.TryGetValue(eventName, out List<(IOnEvent, GameObject)> eventListener))
        {
            eventListener.Add((listener, listenerObj));
        }
        else
        {
            eventListener = new List<(IOnEvent, GameObject)>();
            eventListener.Add((listener, listenerObj));
            _listeners.Add(eventName, eventListener);
        }
    }
    
    public void RemoveListener(string eventName, IOnEvent listener, GameObject listenerObj = null)
    {
        if (_listeners.TryGetValue(eventName, out List<(IOnEvent, GameObject)> eventListener))
        {
            eventListener.Remove((listener, listenerObj));
        }
        else
        {
            Debug.LogWarning("등록 되지 않은 이벤트의 Listener를 삭제하려고 시도했습니다.");
        }
    }
    
    /// <summary>
    /// Key : Event Name
    /// Value : 해당 Event Name의 메세지 큐
    /// </summary>
    private Dictionary<string, Queue<EventMessage>> _eventQueues = new();
    
    /// <summary>
    /// Key : 수신 받을 Event Name
    /// Value : Key를 수신 받는 Listener
    /// </summary>
    private readonly IDictionary<string, List<(IOnEvent, GameObject)>> _listeners = new Dictionary<string, List<(IOnEvent, GameObject)>>();
}
