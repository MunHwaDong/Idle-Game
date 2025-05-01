using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EventMessage
{
    public EventMessage(string eventName)
    {
        EventName = eventName;
    }

    public void AddParameter<T>(object data)
    {
        if (data is null) throw new Exception("이벤트 메세지에 null을 추가하려했습니다.");

        if (_parameters.TryGetValue(typeof(T), out var parameters))
        {
            parameters.Add(data);
        }
        else
        {
            _parameters.Add(typeof(T), new List<object> { data });
        }
    }
    
    public void AddParameter(Type type, object data)
    {
        if (data is null) throw new Exception("이벤트 메세지에 null을 추가하려했습니다.");

        if (_parameters.TryGetValue(type, out var parameters))
        {
            parameters.Add(data);
        }
        else
        {
            _parameters.Add(type, new List<object> { data });
        }
    }
    
    public bool TryGetParameter<T>(out List<T> value)
    {
        if (_parameters.TryGetValue(typeof(T), out var val))
        {
            value = val.Cast<T>().ToList();
            return true;
        }
        else
        {
            value = null;
            return false;
        }
    }

    public List<T> GetParameter<T>()
    {
        if (_parameters.TryGetValue(typeof(T), out var value))
        {
            try
            {
                return value.Cast<T>().ToList();
            }
            catch (InvalidCastException)
            {
                Debug.LogError("메세지에 없는 타입의 파라미터를 요청했습니다.");
                throw;
            }
        }
        else 
            throw new KeyNotFoundException($"\"{typeof(T)}\"" + " is Invalid parameter type");
    }
    
    public string EventName { get; }

    /// <summary>
    /// Key : _eventName을 수신 받는 Listener들에게 전달할 데이터 타입
    /// Value : 중복된 타입을 메세지에 여러개 담을 수 있도록 List로 구현
    /// </summary>
    private readonly Dictionary<Type, List<object>> _parameters = new();
}
