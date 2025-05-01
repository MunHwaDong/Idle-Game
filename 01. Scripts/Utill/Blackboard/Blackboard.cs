using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Blackboard
{
    public void WriteData(Type value, object data)
    {
        _datas[value] = data;

        if (_onChageDataEvents.TryGetValue(value, out UnityEvent changeEvent))
            changeEvent?.Invoke();
    }

    public void RemoveData(Type value)
    {
        _datas.Remove(value);
    }

    public void ListenDataChange(Type value, UnityAction listener)
    {
        UnityEvent thisEvent;

        if (_onChageDataEvents.TryGetValue(value, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new UnityEvent();
            _onChageDataEvents.Add(value, thisEvent);
            thisEvent.AddListener(listener);
        }
    }

    public T ReadData<T>()
    {
        if (_datas.TryGetValue(typeof(T), out object data)) return (T)data;
        else
        {
            Debug.LogError($"\"{typeof(T)}\" is not found");
            throw new KeyNotFoundException();
        }
    }

    public bool HasData(Type value)
    {
        return _datas.ContainsKey(value);
    }
    
    private IDictionary<Type, UnityEvent> _onChageDataEvents = new Dictionary<Type, UnityEvent>();
    private IDictionary<Type, object> _datas = new Dictionary<Type, object>();
}

