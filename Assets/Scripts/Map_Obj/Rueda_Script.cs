using ChrisTutorials.Persistent;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rueda_Script : MonoBehaviour
{
    private Rigidbody rb;
    public float force = 20f;

    public AudioClip scrapStone;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        AudioManager.Instance.PlayLoop3D(scrapStone, transform, 1f, 1, false, 1f, 50f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (rb)
        {
            if(rb.velocity.x > 0)
            {
                rb.AddForce(force,0,0,ForceMode.Force);
            }
            if (rb.velocity.x < 0)
            {
                rb.AddForce(-force, 0, 0, ForceMode.Force);
            }
            //rb.AddForce(Input.GetAxis("Vertical") * force, 0, Input.GetAxis("Horizontal") * force);
        }
    }
}
