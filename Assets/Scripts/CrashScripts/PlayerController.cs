using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //Referencias desde animación y Scripting

    //https://www.youtube.com/watch?v=qwHJgYnoxEY

    private Animator _animator;

    private CharacterController _characterController;

    public float Speed = 5.0f;


    public float RotationSpeed = 240.0f;

    private float Gravity = 10f;

    public float jumpForce = 6f;

    private Vector3 _moveDir = Vector3.zero;



    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();

        _characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Get Input 
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        //if(_characterController.isGrounded && Input.GetButtonDown("Jump"))
        //{
        //    playerJump();
        //}

        //Limit forward movement

        if (v < 0)
            v = 0;

        transform.Rotate(0, h * RotationSpeed * Time.deltaTime, 0);

        if (_characterController.isGrounded)
        {
            bool move = (v > 0) || (h != 0);

            _animator.SetBool("run", move);

            _moveDir = Vector3.forward * v;

            _moveDir = transform.TransformDirection(_moveDir);
            _moveDir *= Speed;


        }

        if (_characterController.isGrounded)
        {
            if (Input.GetButton("Jump"))
            {
                _animator.SetBool("is_in_air", true);
                _moveDir.y = jumpForce;
            }
            else
            {
                _animator.SetBool("is_in_air", false);
                _animator.SetBool("run", _moveDir.magnitude > 0);
            }
        }

        _moveDir.y -= Gravity*Time.deltaTime;

        _characterController.Move(_moveDir * Time.deltaTime);
    }

    public void playerJump()
    {
        _moveDir.y = jumpForce;

        _characterController.Move(_moveDir * Time.deltaTime);
    }
}
