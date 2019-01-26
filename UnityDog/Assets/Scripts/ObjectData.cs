using UnityEngine;

[CreateAssetMenu(fileName = "ObjectData", menuName = "Data/ObjectData", order = 1)]
public class ObjectData : ScriptableObject
{
    [SerializeField] private Actor.Action[] validActions;

    public Actor.Action GetRandomValidAction()
    {
        int index = Random.Range(0, validActions.Length);
        return validActions[index];
    }
}
