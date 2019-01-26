using System;
using System.Collections.Generic;
using UnityEngine;

public class GoalManager : MonoBehaviour
{
    [Serializable]
    private struct Goal
    {
        public Actor.Action action;
        public ObjectData objectData;
        public ZoneManager.ZoneId zoneId;

        public Goal(Actor.Action inAction, ObjectData inObjectData, ZoneManager.ZoneId inZoneId)
        {
            action = inAction;
            objectData = inObjectData;
            zoneId = inZoneId;
        }
    }

    [SerializeField] private ObjectData[] objectData;

    private int goalIndex;
    private List<Goal> goals = new List<Goal>();

    private Action<Actor.Action, ObjectData, Vector3> onPlayerActionDelegate;

    private void Awake()
    {
        onPlayerActionDelegate = OnPlayerAction;
        EventManager.onDogAction.Register(onPlayerActionDelegate);

        InitializeGoals();
    }

    private void OnDestroy()
    {
        EventManager.onDogAction.Unregister(onPlayerActionDelegate);
    }

    private void InitializeGoals()
    {
        foreach (ObjectData data in objectData)
        {
            goals.Add(new Goal(data.GetRandomValidAction(), data, data.GetRandomValidZoneId()));
        }

        Shuffle(goals);
    }

    private void OnPlayerAction(Actor.Action action, ObjectData objectData, Vector3 location)
    {
        Goal goal = goals[goalIndex];
        if (action != goal.action ||
            objectData.name != goal.objectData.name ||
            !ZoneManager.IsPointInZone(location, goal.zoneId))
        {
            return;
        }

        Debug.Log("GOOOOOOOAL!");

        //TODO: reward points/time

        ++goalIndex;
    }

    //Fisher-Yates shuffle
    public void Shuffle<T>(IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = UnityEngine.Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
