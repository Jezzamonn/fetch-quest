using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    public FollowInteractable Arrow;
    [SerializeField] private float actionTime;

    private Dictionary<Collider, InteractableObject> nearbyObjects = new Dictionary<Collider, InteractableObject>();
    private InteractableObject closestObject;

    private bool gameRunning;
    private GoalData.Action lastAction;
    private readonly Timer actionTimer = new Timer();

    private void Awake()
    {
        EventManager.onGameStart.Register(GameStateToggle);
        EventManager.onGameEnd.Register(GameStateToggle);
    }

    private void OnDestroy()
    {
        EventManager.onGameStart.Unregister(GameStateToggle);
        EventManager.onGameEnd.Unregister(GameStateToggle);
    }

    private void GameStateToggle()
    {
        gameRunning = !gameRunning;
    }

    private void Update()
    {
        if (!gameRunning)
        {
            return;
        }

        bool wasDone = actionTimer.IsDone();
        actionTimer.Tick(Time.deltaTime);
        if (!actionTimer.IsDone())
        {
            return;
        }

        if (!wasDone)
        {
            EventManager.onDogActionEnd.Dispatch(lastAction, GoalData.ObjectId.NONE, Vector3.zero);
        }

        UpdateClosest();

        //process input
        if (closestObject != null && Input.GetButtonDown("ContextAction"))
        {
            lastAction = GoalData.Action.Context;
            actionTimer.Start(actionTime);

            EventManager.onDogAction.Dispatch(lastAction, closestObject.objectId, transform.position);
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

        Arrow.Target = closestObject;
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
