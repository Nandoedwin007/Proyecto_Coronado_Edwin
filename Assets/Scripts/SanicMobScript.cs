﻿using ChrisTutorials.Persistent;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SanicMobScript : MonoBehaviour
{



    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;

    public AudioClip walkingSound;
    public AudioClip animalSound;

    //Codigo utilizado de https://docs.unity3d.com/Manual/nav-AgentPatrol.html
    // Start is called before the first frame update
    void Start()
    {

        if (walkingSound != null)
            AudioManager.Instance.PlayLoop3D(walkingSound, transform, 1f, 1, false, 1f, 50f);
        if (animalSound != null)
            AudioManager.Instance.PlayLoop3D(animalSound, transform, 1f, 1, false, 1f, 50f);


        agent = GetComponent<NavMeshAgent>();

        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        agent.autoBraking = true;

        GotoNextPoint();




    }

    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        agent.destination = points[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
    }
    

    // Update is called once per frame
    void Update()
    {

        // Choose the next destination point when the agent gets
        // close to the current one.
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            GotoNextPoint();

    }
}
