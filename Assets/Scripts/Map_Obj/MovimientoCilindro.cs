using ChrisTutorials.Persistent;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoCilindro : MonoBehaviour
{
    public int tipoCilindro = 1;

    private Vector3 startPos;
    private Vector3 endPos;

    private bool bajando = true;
    private float paso = 0.1f;

    public AudioClip scrapStone;


    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        endPos = startPos - new Vector3(0, 10, 0);
        //Debug.Log("Poisición Inicial: " + startPos.ToString());
        if(scrapStone!=null)
            AudioManager.Instance.PlayLoop3D(scrapStone, transform, 1f, 1, false, 1f, 50f);
    }

    // Update is called once per frame
    void Update()
    {
        if (tipoCilindro == 1)
        {
            //Movimiento lineal pero difernte al bajar y al subir
            if (transform.position.y <= endPos.y)
            {
                bajando = false;
                paso = 0.1f;
            }
                
            if (transform.position.y >= startPos.y)
            {
                bajando = true;
                paso = 0.4f;
            }
                


            if (bajando == true)
            {
                transform.position = transform.position - new Vector3(0, paso, 0);
            }
            if (bajando == false)
            {
                transform.position = transform.position + new Vector3(0, paso, 0);
            }
            

        }
        else if (tipoCilindro == 2)
        {
            //Movimiento senoidal
            transform.position = startPos + new Vector3(0, Mathf.Sin(2 * Time.time) * 5, 0);

        }
    }
}
