using System;
using System.Collections.Generic;
using UnityEngine;

public static class ZoneManager
{
    public static readonly Dictionary<GoalData.ZoneId, Collider> zones = new Dictionary<GoalData.ZoneId, Collider>();

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

        return zones[zoneId].bounds.Contains(point);
    }
}
