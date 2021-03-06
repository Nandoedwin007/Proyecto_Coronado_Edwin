﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaGanarScript : MonoBehaviour
{
    private Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, Time.deltaTime * 10);
        transform.position = startPos + new Vector3(0, 0.3f * Mathf.Sin(Time.time), 0);
    }
}
