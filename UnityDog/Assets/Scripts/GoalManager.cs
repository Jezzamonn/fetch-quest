using System;
using System.Collections.Generic;
using UnityEngine;
using EnumExt;

public class GoalManager : MonoBehaviour
{
    [Serializable]
    private struct Goal
    {
        public GoalData.Action action;
        public GoalData.ObjectId objectId;
        public GoalData.ZoneId zoneId;

        public Goal(GoalData.Action inAction, GoalData.ObjectId inObjectId, GoalData.ZoneId inZoneId)
        {
            action = inAction;
            objectId = inObjectId;
            zoneId = inZoneId;
        }

        public string Description
        {
            get
            {
                return objectId.GetDescription();
            }
        }
    }

    [SerializeField] private GoalData[] goalData;
    [ReadOnly][SerializeField] private GoalData.ObjectId currentGoalItem;

    private int goalIndex;
    private List<Goal> goals = new List<Goal>();

    private Goal CurrentGoal
    {
        get
        {
            return goals[goalIndex];
        }
    }

    private Action<GoalData.Action, GoalData.ObjectId, Vector3> onPlayerActionDelegate;

    private void Awake()
    {
        onPlayerActionDelegate = OnPlayerAction;
        EventManager.onDogAction.Register(onPlayerActionDelegate);

        InitializeGoals();
    }

    private void Start()
    {
        NotifyCurrentGoal();
    }

    private void OnDestroy()
    {
        EventManager.onDogAction.Unregister(onPlayerActionDelegate);
    }

    private void InitializeGoals()
    {
        foreach (GoalData data in goalData)
        {
            goals.Add(new Goal(data.GetRandomValidAction(), data.requiredObject, data.GetRandomValidZoneId()));
        }

        Shuffle(goals);
        currentGoalItem = goals[goalIndex].objectId;
    }

    private void OnPlayerAction(GoalData.Action action, GoalData.ObjectId objectId, Vector3 location)
    {
        Goal goal = CurrentGoal;

        if (action != goal.action ||
            objectId != goal.objectId ||
            !ZoneManager.IsPointInZone(location, goal.zoneId))
        {
            return;
        }

        //TODO: reward points/time

        ++goalIndex;

        currentGoalItem = goals[goalIndex].objectId;

        NotifyCurrentGoal();
    }

    public void NotifyCurrentGoal()
    {
        string desc = CurrentGoal.Description;
        Debug.Log(desc);
        NetworkManager.instance.SendGoal(desc);
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
