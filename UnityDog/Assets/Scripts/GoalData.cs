using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GoalData", menuName = "Data/GoalData", order = 1)]
public class GoalData : ScriptableObject
{
    public enum Action
    {
        Context,
    }

    [Serializable]
    public enum ObjectId
    {
        NONE,
        Stick,
        Bed,
    }

    [Serializable]
    public enum ZoneId
    {
        ANY,
        Kitchen,
        LivingRoom,
        Yard
    }

    public ObjectId requiredObject;
    [SerializeField] private Action[] validActions;
    [SerializeField] private ZoneId[] validZones;

    public Action GetRandomValidAction()
    {
        int index = UnityEngine.Random.Range(0, validActions.Length);
        return validActions[index];
    }

    public ZoneId GetRandomValidZoneId()
    {
        int index = UnityEngine.Random.Range(0, validZones.Length);
        return validZones[index];
    }
}
