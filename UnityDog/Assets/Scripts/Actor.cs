using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    public enum Action
    {
        Context
    }

    private Dictionary<Collider, Interactable> nearbyObjects = new Dictionary<Collider, Interactable>();
    private Interactable closestObject;

    private void Update()
    {
        UpdateClosest();

        //process input
        if (closestObject != null && Input.GetButtonDown("ContextAction"))
        {
            EventManager.onDogAction.Dispatch(Action.Context, closestObject, 0);
            closestObject.OnInteract();
        }
    }

    private void UpdateClosest()
    {
        Interactable newClosest = null;
        float minDistance = float.MaxValue;
        foreach (KeyValuePair<Collider, Interactable> thing in nearbyObjects)
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
        Interactable interactable = other.GetComponent<Interactable>();
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
