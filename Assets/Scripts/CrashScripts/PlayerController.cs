using ChrisTutorials.Persistent;
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

    private bool isSpinning;

    public AudioClip spinClip;

    public AudioClip whoaClip;

    private bool whoaPlayed = false;



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

        if (Input.GetKeyDown("c"))
        {
            
            _animator.SetBool("spin", true);
            isSpinning = true;
            if(spinClip != null)
            {
                AudioManager.Instance.Play(spinClip, transform,0.2f);
            }
            
        }
        else
        {
            _animator.SetBool("spin", false);
            isSpinning = false;
        }

        _moveDir.y -= Gravity*Time.deltaTime;

        _characterController.Move(_moveDir * Time.deltaTime);
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    //Solo se necesita 1 power up para sobrevivir la lava
    //    if (collision.gameObject.tag == "Crush")
    //    {
    //        _animator.SetBool("isCrushed", true);
    //        AudioManager.Instance.Play(whoaClip, transform);
    //        //Destroy(gameObject);
    //    }
    //}
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //Solo se necesita 1 power up para sobrevivir la lava
        if (hit.gameObject.tag == "Crush" && whoaPlayed == false)
        {
            whoaPlayed = true;
            _animator.SetBool("isCrushed", true);
            AudioManager.Instance.Play(whoaClip, transform);
            _characterController.enabled = false;
            //Destroy(gameObject);
        }

        //Solo se necesita 1 power up para sobrevivir la lava
        if (hit.gameObject.tag == "Water" && whoaPlayed == false)
        {
            whoaPlayed = true;
            _animator.SetBool("isDrowning", true);
            AudioManager.Instance.Play(whoaClip, transform);
            _characterController.enabled = false;
            //Destroy(gameObject);
        }
    }

    private void playerJump()
    {
        _moveDir.y = jumpForce;

        _characterController.Move(_moveDir * Time.deltaTime);
    }
}
