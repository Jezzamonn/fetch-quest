using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    private Dictionary<Collider, InteractableObject> nearbyObjects = new Dictionary<Collider, InteractableObject>();
    private InteractableObject closestObject;

    private void Update()
    {
        UpdateClosest();

        //process input
        if (closestObject != null && Input.GetButtonDown("ContextAction"))
        {
            EventManager.onDogAction.Dispatch(GoalData.Action.Context, closestObject.objectId, transform.position);
            closestObject.OnInteract();
        }
    }

    private void UpdateClosest()
    {
        InteractableObject newClosest = null;
        float minDistance = float.MaxValue;
        foreach (KeyValuePair<Collider, InteractableObject> thing in nearbyObjects)
        {
            float distance = (transform.position - thing.Key.ClosestPointOnBounds(transform.position)).sqrMagnitude;
            if (distance < minDistance)
            {
                newClosest = thing.Value;
                minDistance = distance;
            }
        }

        if (newClosest == closestObject)
        {
            return;
        }

        closestObject?.NotContextObject();
        newClosest?.IsContextObject();

        closestObject = newClosest;
    }

    private void OnTriggerEnter(Collider other)
    {
        InteractableObject interactable = other.GetComponent<InteractableObject>();
        if (interactable != null && !nearbyObjects.ContainsKey(other))
        {
            nearbyObjects.Add(other, interactable);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (nearbyObjects.ContainsKey(other))
        {
            nearbyObjects.Remove(other);
        }
    }
}
