using ChrisTutorials.Persistent;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicioScript : MonoBehaviour
{
    //public GameObject canvasUI_2D;
    //public GameObject gameContoller;
    //public AudioClip linkStarto;
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CambiarEscena(string _newScene)
    {

        //AudioManager.Instance.Play3D(linkStarto, transform,1f,1f,1,100);
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(_newScene);

    }
}
