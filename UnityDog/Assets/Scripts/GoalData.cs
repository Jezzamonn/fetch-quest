using System;
using UnityEngine;
using System.ComponentModel;

[CreateAssetMenu(fileName = "GoalData", menuName = "Data/GoalData", order = 1)]
public class GoalData : ScriptableObject
{
    public enum Action
    {
        Context,
        Drop,
    }

    [Serializable]
    public enum ObjectId
    {
        NONE,
        [Description("a stick")]
        Stick,
        [Description("the bed")]
        Bed,
        [Description("a bone")]
        Bone,
        [Description("food")]
        Food,
        [Description("the fridge")]
        Fridge,
        [Description("the vase")]
        Vase,
        [Description("a fern")]
        Fern,
        [Description("a tree")]
        Tree,
    }

    [Serializable]
    public enum ZoneId
    {
        ANY,
        [Description("kitchen")]
        Kitchen,
        [Description("living room")]
        LivingRoom,
        [Description("yard")]
        Yard,
        [Description("master bedroom")]
        Bedroom1,
        [Description("small bedroom")]
        Bedroom2,
    }

    public ObjectId requiredObject = ObjectId.NONE;
    [SerializeField] private Action[] validActions = { Action.Context };
    [SerializeField] private ZoneId[] validZones = { ZoneId.ANY };

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
