using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHider : MonoBehaviour
{

    public GameObject top;
    public Transform tuna;

    public float cutOffPosition = 1f;

    // Update is called once per frame
    void Update()
    {
        if (tuna.position.y > cutOffPosition) {
            top.active = true;
        }
        else {
            top.active = false;
        }
    }
}
