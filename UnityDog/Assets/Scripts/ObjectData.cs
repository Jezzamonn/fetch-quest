using UnityEngine;

[CreateAssetMenu(fileName = "ObjectData", menuName = "Data/ObjectData", order = 1)]
public class ObjectData : ScriptableObject
{
    [SerializeField] private Actor.Action[] validActions;
    [SerializeField] private ZoneManager.ZoneId[] validZones;

    public Actor.Action GetRandomValidAction()
    {
        int index = Random.Range(0, validActions.Length);
        return validActions[index];
    }

    public ZoneManager.ZoneId GetRandomValidZoneId()
    {
        int index = Random.Range(0, validZones.Length);
        return validZones[index];
    }
}
