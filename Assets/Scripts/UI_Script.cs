using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Script : MonoBehaviour
{

    //Referencia del código https://www.youtube.com/watch?v=urNrY7FgMao

    public Transform PlayerTransform;

    private Vector3 _cameraOffset;

    [Range(0.01f, 1.0f)]
    public float SmootFactor = 0.5f;

    public bool LookAtPlayer = false;

    public bool RotateAroundPlayer = true;

    public float RotationSpeed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        _cameraOffset = transform.position - PlayerTransform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {

        if (RotateAroundPlayer)
        {
            Quaternion camTurnAngle =
                Quaternion.AngleAxis(Input.GetAxis("Mouse X") * RotationSpeed , Vector3.down);
            //camTurnAngle.x *= -1;
            camTurnAngle.y *= -1;
            //camTurnAngle.z *= -1;
            //camTurnAngle.w *= -1;

            _cameraOffset = camTurnAngle * _cameraOffset;
            //_cameraOffset.y *= -1;
        }
        Vector3 newPos = PlayerTransform.position + _cameraOffset;

        transform.position = Vector3.Slerp(transform.position, newPos, SmootFactor);



        if (LookAtPlayer || RotateAroundPlayer)
            transform.LookAt(PlayerTransform,Vector3.up);
        //transform.
    }
}
