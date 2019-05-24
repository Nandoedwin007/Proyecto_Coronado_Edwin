using ChrisTutorials.Persistent;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public AudioClip backgroundMusic;
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlayLoop(backgroundMusic, transform, 0.4f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
