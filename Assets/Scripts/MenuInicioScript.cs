using ChrisTutorials.Persistent;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicioScript : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //Función que cambia de escena usando como parámetro el nombre de la escena
    public void CambiarEscena(string _newScene)
    {


        Time.timeScale = 1.0f;
        SceneManager.LoadScene(_newScene);

    }
}
