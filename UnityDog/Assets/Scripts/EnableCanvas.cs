﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableCanvas : MonoBehaviour
{
    public GameObject canvas;

    void Awake()
    {
        canvas.active = true;
    }
}
