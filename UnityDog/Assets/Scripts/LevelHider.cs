using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHider : MonoBehaviour
{

    public GameObject top;
    public Transform tuna;
    public SpriteRenderer sprite;

    public float downStairsCutOffPosition = 1f;
    public float upStairsCutOffPosition = 1f;

    // Update is called once per frame
    void Update()
    {
        top.SetActive(tuna.position.y > downStairsCutOffPosition);

        // Update Tuna's sprite layer?
        if (tuna.position.y > upStairsCutOffPosition)
        {
            sprite.sortingLayerName = "TopObj";
        }
        else if (tuna.position.y > downStairsCutOffPosition)
        {
            sprite.sortingLayerName = "MiddleObj";
        }
        else
        {
            sprite.sortingLayerName = "BottomObj";
        }
    }
}
