using ChrisTutorials.Persistent;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    public AudioClip standarCrateBreakClip;

    public AudioClip wumpaFruitEatClip;

    public AudioClip youDiedClip;

    private bool whoaPlayed = false;

    public GameObject wumpaFruitPrefab;

    //public Text youDiedText;

    


    private GameObject gameController;
    private GameController gameControllerScript;

    private bool isDead = false;

    //Referenciamos el texto para la animación de Muerte
    public GameObject youDiedTextObject;
    private Animator textAnimator;
    private Text youDiedText;

    //Referenciamos el fondo negro para la animación de Muerte
    public GameObject youDiedImageObject;
    private Animator youDiedImageAnimator;
    private Image youDiedImage;

    //private Canvas uiCanvas;




    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();

        _characterController = GetComponent<CharacterController>();

        gameController = GameObject.Find("GameController");

        //uiCanvas = GameObject.Find("UI_Canvas_2D");
        gameControllerScript = gameController.GetComponent<GameController>();

        DontDestroyOnLoad(gameController);


        if (youDiedTextObject!=null)
        {
            textAnimator = youDiedTextObject.GetComponent<Animator>();
            textAnimator.enabled = false;
        }
            
        if (youDiedTextObject != null)
        {
            youDiedText = youDiedTextObject.GetComponent<Text>();
            youDiedText.enabled = false;
        }

        if (youDiedImageObject != null)
        {
            youDiedImageAnimator = youDiedImageObject.GetComponent<Animator>();
            youDiedImageAnimator.enabled = false;
        }

        if (youDiedImageObject != null)
        {
            youDiedImage = youDiedImageObject.GetComponent<Image>();
            youDiedImage.enabled = false;
        }


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
            if(spinClip != null)
            {
                AudioManager.Instance.Play(spinClip, transform,0.2f);
            }
            
        }
        else
        {
            _animator.SetBool("spin", false); 
        }

        //Verifico si está activada la animación de Spinning, en caso de que si activa el Bool respectivo
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Armature|SpinGood"))
        {
            //Debug.Log("isSpinning TRUE");
            isSpinning = true;
            _characterController.radius = 0.6f;
        }
        else
        {
           //Debug.Log("isSpinning FALSE");
            _characterController.radius = 0.26f;
            isSpinning = false;
        }

        _moveDir.y -= Gravity*Time.deltaTime;

        _characterController.Move(_moveDir * Time.deltaTime);

        if(isDead == true)
        {
            isDead = false;
            Invoke("playYouDied", 2f);
            
        }

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
        //Detectamos si el objeto tiene la etiqueta Crush, en caso de que si se muere crash aplastado
        if (hit.gameObject.tag == "Crush" && whoaPlayed == false)
        {

            if (whoaClip != null)
            {
                whoaPlayed = true;
                _animator.SetBool("isCrushed", true);
                AudioManager.Instance.Play(whoaClip, transform);
                _characterController.enabled = false;
                isDead = true;
            }
            

        }

        //Verificamos si el objeto es agua, si es agua se muere Crash ahogado
        if (hit.gameObject.tag == "Water" && whoaPlayed == false)
        {
            if (whoaClip != null)
            {
                whoaPlayed = true;
                _animator.SetBool("isDrowning", true);
                AudioManager.Instance.Play(whoaClip, transform);
                _characterController.enabled = false;
                isDead = true;
            }

            
 
        }

        //Verificamos si el objeto es el StandardCrate, en caso de que sí y esté girando, se destruye
        //Así mismo, instanciamos un Prefab de WumpaFruit
        if (hit.gameObject.tag == "StandardCrate" && isSpinning == true)
        {


            if (standarCrateBreakClip != null)
            {
                AudioManager.Instance.Play(standarCrateBreakClip, transform,1f);

                Instantiate(wumpaFruitPrefab, hit.gameObject.transform.position, Quaternion.identity);

                Destroy(hit.gameObject);


            }
            
            
        }

        //Verificamos si el objeto es el WumpaFruit, en caso de que si se hace un Random Para saber cuántos WumpaFruits
        //Recogió Crash
        if (hit.gameObject.tag == "WumpaFruit")
        {

            if (wumpaFruitEatClip != null)
            {
                //Intento para que sonara un AudioClip al finalizar el otro
                ////Random.Range(2,25)
                //for (int i = 0; i <= Random.Range(2, 25); i++)
                //{
                //    AudioManager.Instance.Play(wumpaFruitEatClip, transform, 1f, 1f);
                //    Invoke("playWumpaSound", 2f);
                //    //StartCoroutine(WaitSomeTime());
                //    //StopCoroutine(WaitSomeTime());

                //}


                //Solo suena 1 vez sin importar cantidad de Wumpas
                gameControllerScript.wumpaCounter += Random.Range(1, 25);
                AudioManager.Instance.Play(wumpaFruitEatClip, transform, 1f);

                Destroy(hit.gameObject);


            }
        }
    }



    private void playYouDied()
    {
        AudioManager.Instance.Play(youDiedClip, transform, 1f, 1f);
        gameControllerScript.livesCounter -= 1;

        if (textAnimator != null)
        {
            textAnimator.enabled = true;
        }

        if (youDiedImageAnimator != null)
        {
            youDiedImageAnimator.enabled = true;
        }
        Invoke("RestartLevel", 3f);

    }

    private void RestartLevel()
    {
        if (textAnimator != null)
        {
            textAnimator.enabled = false;
            youDiedText.enabled = false;
        }

        if (youDiedImageAnimator != null)
        {
            youDiedImageAnimator.enabled = false;
            youDiedImage.enabled = false;
        }


        DontDestroyOnLoad(gameController);

        SceneManager.LoadScene("Nivel_1");
    }

    IEnumerator WaitSomeTime()
    {
        //audio.clip = engineStartClip;
        //audio.Play();
        //AudioManager.Instance.Play(wumpaFruitEatClip, transform, 1f, 1f);
        print(Time.time);
        gameControllerScript.wumpaCounter += 1;
        AudioManager.Instance.PlayOne(wumpaFruitEatClip, transform, 1f, 1f);
        yield return new WaitForSeconds(5f);
        AudioManager.Instance.PlayOne(wumpaFruitEatClip, transform, 1f, 1f);
        
        print(Time.time);
        //audio.clip = engineLoopClip;
        //audio.Play();
    }

    private void playerJump()
    {
        _moveDir.y = jumpForce;

        _characterController.Move(_moveDir * Time.deltaTime);
    }
}
