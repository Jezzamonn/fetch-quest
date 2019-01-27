﻿using System;
using System.Collections.Generic;
using UnityEngine;
using EnumExt;
using UnityEngine.UI;

public class GoalManager : MonoBehaviour
{
    private static string TIME_FORMAT = "{0} : {1}";

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

    [Space(20)]

    [SerializeField] private float totalTime;
    [SerializeField] private int pointsPerGoal;

    [SerializeField] private ScoreUI scoreUI;
    [SerializeField] private Text timeUI;

    private int goalIndex;
    private List<Goal> goals = new List<Goal>();

    private int score;
    private readonly Timer gameTimer = new Timer();

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
        gameTimer.Start(totalTime);

        scoreUI.UpdateScore(score);
    }

    private void Start()
    {
        NotifyCurrentGoal();
    }

    private void Update()
    {
        gameTimer.Tick(Time.deltaTime);

        float timeRemaining = gameTimer.Remaining();
        float m = (int)(timeRemaining / 60);
        float s = (int)(timeRemaining % 60);
        timeUI.text = string.Format(TIME_FORMAT, m.ToString("00"), s.ToString("00"));

        if (gameTimer.IsDone())
        {
            //TODO: GAME OVER
        }
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

        score += pointsPerGoal;
        scoreUI.UpdateScore(score);

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
