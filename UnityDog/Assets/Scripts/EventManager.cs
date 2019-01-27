using UnityEngine;

public static class EventManager
{
    public static readonly CallbackEvent<GoalData.Action, GoalData.ObjectId, Vector3> onDogAction =
        new CallbackEvent<GoalData.Action, GoalData.ObjectId, Vector3>();

    public static readonly CallbackEvent<GoalData.Action, GoalData.ObjectId, Vector3> onDogActionEnd =
        new CallbackEvent<GoalData.Action, GoalData.ObjectId, Vector3>();

    public static readonly CallbackEvent onGoalDone =
        new CallbackEvent();

    public static readonly CallbackEvent onGameStart =
        new CallbackEvent();

    public static readonly CallbackEvent onGameEnd =
        new CallbackEvent();
}

// Generic event classes
public class CallbackEvent
{
    public System.Action callbacks;

    public void Register(System.Action callback)
    {
        callbacks += callback;
    }

    public void Unregister(System.Action callback)
    {
        callbacks -= callback;
    }

    public void Dispatch()
    {
        callbacks?.Invoke();
    }

    public void UnregisterAll()
    {
        callbacks = null;
    }
}

public class CallbackEvent<T>
{
    public System.Action<T> callbacks;

    public void Register(System.Action<T> callback)
    {
        callbacks += callback;
    }

    public void Unregister(System.Action<T> callback)
    {
        callbacks -= callback;
    }

    public void Dispatch(T data)
    {
        callbacks?.Invoke(data);
    }

    public void UnregisterAll()
    {
        callbacks = null;
    }
}

public class CallbackEvent<T1, T2>
{
    public System.Action<T1, T2> callbacks;

    public void Register(System.Action<T1, T2> callback)
    {
        callbacks += callback;
    }

    public void Unregister(System.Action<T1, T2> callback)
    {
        callbacks -= callback;
    }

    public void Dispatch(T1 data1, T2 data2)
    {
        callbacks?.Invoke(data1, data2);
    }

    public void UnregisterAll()
    {
        callbacks = null;
    }
}

public class CallbackEvent<T1, T2, T3>
{
    public System.Action<T1, T2, T3> callbacks;

    public void Register(System.Action<T1, T2, T3> callback)
    {
        callbacks += callback;
    }

    public void Unregister(System.Action<T1, T2, T3> callback)
    {
        callbacks -= callback;
    }

    public void Dispatch(T1 data1, T2 data2, T3 data3)
    {
        callbacks?.Invoke(data1, data2, data3);
    }

    public void UnregisterAll()
    {
        callbacks = null;
    }
}

public class CallbackEvent<T1, T2, T3, T4>
{
    public System.Action<T1, T2, T3, T4> callbacks;

    public void Register(System.Action<T1, T2, T3, T4> callback)
    {
        callbacks += callback;
    }

    public void Unregister(System.Action<T1, T2, T3, T4> callback)
    {
        callbacks -= callback;
    }

    public void Dispatch(T1 data1, T2 data2, T3 data3, T4 data4)
    {
        callbacks?.Invoke(data1, data2, data3, data4);
    }

    public void UnregisterAll()
    {
        callbacks = null;
    }
}
