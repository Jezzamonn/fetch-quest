using System;
using System.Collections.Generic;
using UnityEngine;

public static class ZoneManager
{
    [Serializable]
    public enum ZoneId
    {
        ANY,
        Kitchen,
        LivingRoom,
        Yard
    }

    public static readonly Dictionary<ZoneId, Collider> zones = new Dictionary<ZoneId, Collider>();

    public static bool IsPointInZone(Vector3 point, ZoneId zoneId)
    {
        if (zoneId == ZoneId.ANY)
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
