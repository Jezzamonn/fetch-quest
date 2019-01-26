using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Zone : MonoBehaviour
{
    [SerializeField] private ZoneManager.ZoneId zoneId;

    private void Awake()
    {
        Collider collider = GetComponent<Collider>();

        if (ZoneManager.zones.ContainsKey(zoneId))
        {
            Debug.LogErrorFormat("Tried to add zoneId {0} for {1} that already exists for {2}.",
                zoneId,
                collider.gameObject.name,
                ZoneManager.zones[zoneId].gameObject.name);
        }

        ZoneManager.zones.Add(zoneId, collider);
    }

    private void OnDestroy()
    {
        ZoneManager.zones.Remove(zoneId);
    }
}
