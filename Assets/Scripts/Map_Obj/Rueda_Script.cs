using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rueda_Script : MonoBehaviour
{
    private Rigidbody rb;
    public float force = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
