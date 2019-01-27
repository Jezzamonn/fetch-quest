using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Zone : MonoBehaviour
{
    [SerializeField] private GoalData.ZoneId zoneId;

    private void Awake()
    {
        Collider collider = GetComponent<Collider>();

        if (!ZoneManager.zones.ContainsKey(zoneId))
        {
            ZoneManager.zones.Add(zoneId, new List<Collider>());
        }

        ZoneManager.zones[zoneId].Add(collider);
    }

    private void OnDestroy()
    {
        ZoneManager.zones.Remove(zoneId);
    }
}
