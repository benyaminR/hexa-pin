using System;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : SingletonMonoBehaviour<EventManager>
{

    private List<GameEventBase> events = new List<GameEventBase>();



    public void Listen(string eventName, Action callback)
    {
        if (events.Exists(x => x.eventName == eventName))
        {
            var gameEvent = events.Find(x => x.eventName == eventName) as GameEvent;
            gameEvent.Subscribe(callback);
            return;
        }
        else
        {
            var gameEvent = new GameEvent();
            gameEvent.eventName = eventName;
            gameEvent.Subscribe(callback);
            events.Add(gameEvent);
        }
    }


    public void Listen<T1>(string eventName, Action<T1> callback)
    {

        if (events.Exists(x => x.eventName == eventName))
        {
            var gameEvent = events.Find(x => x.eventName == eventName) as GameEvent<T1>;
            gameEvent.Subscribe(callback);
            return;
        }
        else
        {
            var gameEvent = new GameEvent<T1>();
            gameEvent.eventName = eventName;
            gameEvent.Subscribe(callback);
            events.Add(gameEvent);
        }
    }

    public void Listen<T1, T2>(string eventName, Action<T1, T2> callback)
    {

        if (events.Exists(x => x.eventName == eventName))
        {
            var gameEvent = events.Find(x => x.eventName == eventName) as GameEvent<T1, T2>;
            gameEvent.Subscribe(callback);
            return;
        }
        else
        {
            var gameEvent = new GameEvent<T1, T2>();
            gameEvent.eventName = eventName;
            gameEvent.Subscribe(callback);
            events.Add(gameEvent);
        }
    }


    public void Listen<T1, T2, T3>(string eventName, Action<T1, T2, T3> callback)
    {

        if (events.Exists(x => x.eventName == eventName))
        {
            var gameEvent = events.Find(x => x.eventName == eventName) as GameEvent<T1, T2, T3>;
            gameEvent.Subscribe(callback);
            return;
        }
        else
        {
            var gameEvent = new GameEvent<T1, T2, T3>();
            gameEvent.eventName = eventName;
            gameEvent.Subscribe(callback);
            events.Add(gameEvent);
        }
    }


    public void Invoke(string eventName)
    {
        if (events.Exists(x => x.eventName == eventName))
        {
            var gameEvent = events.Find(x => x.eventName == eventName) as GameEvent;
            gameEvent.Invoke();
            Debug.Log("Event Invoked:" + eventName);
        }
        else
        {
            Debug.Log("Event does not exist:" + eventName);
        }
    }


    public void Invoke<T1>(string eventName, T1 type)
    {
        if (events.Exists(x => x.eventName == eventName))
        {
            var gameEvent = events.Find(x => x.eventName == eventName) as GameEvent<T1>;
            gameEvent.Invoke(type);
        }
        else
        {
            Debug.Log("Event does not exist:" + eventName);
        }
    }

    public void Invoke<T1, T2>(string eventName, T1 type1, T2 type2)
    {
        if (events.Exists(x => x.eventName == eventName))
        {
            var gameEvent = events.Find(x => x.eventName == eventName) as GameEvent<T1, T2>;
            gameEvent.Invoke(type1, type2);
        }
        else
        {
            Debug.Log("Event does not exist:" + eventName);
        }
    }

    public void Invoke<T1, T2, T3>(string eventName, T1 type1, T2 type2, T3 type3)
    {
        if (events.Exists(x => x.eventName == eventName))
        {
            var gameEvent = events.Find(x => x.eventName == eventName) as GameEvent<T1, T2, T3>;
            gameEvent.Invoke(type1, type2, type3);
        }
        else
        {
            Debug.Log("Event does not exist:" + eventName);
        }
    }


    private void OnDisable()
    {
        foreach (var gameEvent in events)
        {
            gameEvent.UnsubscribeAll();
        }
    }


}

public abstract class GameEventBase
{
    public string eventName;

    public abstract void UnsubscribeAll();

}

public class GameEvent : GameEventBase
{
    private event Action action;

    public void Invoke()
    {
        this.action?.Invoke();
    }

    public void Subscribe(Action newAction)
    {
        this.action += newAction;
    }

    public void Unsubscribe(Action newAction)
    {
        this.action -= newAction;

    }

    public override void UnsubscribeAll()
    {
        // unsubscribe all actions
        this.action = null;
    }
}


public class GameEvent<T> : GameEventBase
{
    private event Action<T> action;

    public void Invoke(T type)
    {
        this.action?.Invoke(type);
    }

    public void Subscribe(Action<T> action)
    {
        this.action += action;
    }

    public void Unsubscribe(Action<T> action)
    {
        this.action -= action;
    }

    public override void UnsubscribeAll()
    {
        // unsubscribe all actions
        this.action = null;
    }
}

public class GameEvent<T1, T2> : GameEventBase
{
    private event Action<T1, T2> action;

    public void Invoke(T1 type1, T2 type2)
    {
        this.action?.Invoke(type1, type2);
    }

    public void Subscribe(Action<T1, T2> action)
    {
        this.action += action;
    }

    public void Unsubscribe(Action<T1, T2> action)
    {
        this.action -= action;
    }

    public override void UnsubscribeAll()
    {
        // unsubscribe all actions
        this.action = null;
    }
}

public class GameEvent<T1, T2, T3> : GameEventBase
{
    private event Action<T1, T2, T3> action;

    public void Invoke(T1 type1, T2 type2, T3 type3)
    {
        this.action?.Invoke(type1, type2, type3);
    }

    public void Subscribe(Action<T1, T2, T3> action)
    {
        this.action += action;
    }

    public void Unsubscribe(Action<T1, T2, T3> action)
    {
        this.action -= action;
    }

    public override void UnsubscribeAll()
    {
        // unsubscribe all actions
        this.action = null;
    }
}