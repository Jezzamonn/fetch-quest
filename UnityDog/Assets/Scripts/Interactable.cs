using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private Material normalMat;
    [SerializeField] private Material closestMat;

    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void IsContextObject()
    {
        meshRenderer.material = closestMat;
    }

    public void NotContextObject()
    {
        meshRenderer.material = normalMat;
    }
}
