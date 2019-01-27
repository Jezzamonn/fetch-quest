using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public GoalData.ObjectId objectId;

    public float Height = 1f;

    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        NotContextObject();
    }

    public void IsContextObject()
    {
    }

    public void NotContextObject()
    {
    }

    public void OnInteract()
    {
        
    }
}
