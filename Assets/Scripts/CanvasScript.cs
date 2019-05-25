using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasScript : MonoBehaviour
{
    public static CanvasScript Instance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        if (Instance){
            DestroyImmediate(gameObject);
        }     
             else
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
    }
}
