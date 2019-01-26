using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    private Dictionary<Collider, Interactable> nearbyObjects = new Dictionary<Collider, Interactable>();
    private Interactable lastClosest;

    private void Update()
    {
        UpdateClosest();
    }

    private void UpdateClosest()
    {
        Interactable closestObject = null;
        float minDistance = float.MaxValue;
        foreach (KeyValuePair<Collider, Interactable> thing in nearbyObjects)
        {
            float distance = (transform.position - thing.Key.ClosestPointOnBounds(transform.position)).sqrMagnitude;
            if (distance < minDistance)
            {
                closestObject = thing.Value;
                minDistance = distance;
            }
        }

        if (closestObject == lastClosest)
        {
            return;
        }

        lastClosest?.NotContextObject();
        closestObject?.IsContextObject();

        lastClosest = closestObject;
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
