using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableCanvas : MonoBehaviour
{
    public Canvas canvas;

    void Awake()
    {
        canvas.enabled = true;
    }
}
