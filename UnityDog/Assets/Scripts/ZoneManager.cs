using System;
using System.Collections.Generic;
using UnityEngine;

public static class ZoneManager
{
    public static readonly Dictionary<GoalData.ZoneId, List<Collider>> zones = new Dictionary<GoalData.ZoneId, List<Collider>>();

    public static bool IsPointInZone(Vector3 point, GoalData.ZoneId zoneId)
    {
        if (zoneId == GoalData.ZoneId.ANY)
        {
            return true;
        }

        if (!zones.ContainsKey(zoneId))
        {
            return false;
        }

        foreach (Collider collider in zones[zoneId])
        {
            if (collider.bounds.Contains(point))
            {
                return true;
            }
        }

        return false;
    }
}
