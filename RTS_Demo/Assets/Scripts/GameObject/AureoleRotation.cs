﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AureoleRotation : MonoBehaviour
{
    public float speed;

    void Update()
    {
        this.transform.Rotate(new Vector3(0,0,1)*speed);
    }
}

