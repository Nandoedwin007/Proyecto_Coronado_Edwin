﻿using ChrisTutorials.Persistent;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSound : MonoBehaviour
{
    public AudioClip waterMusic;
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlayLoop3D(waterMusic, transform, 0.4f, 1, false, 1f, 150f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
