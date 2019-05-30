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

    public AudioClip nitroCrateExplosionClip;

    public AudioClip youDiedClip;
    public AudioClip tadaClip;
    public AudioClip winninFanfareClip;

    public AudioClip gameOverClip;

    private bool whoaPlayed = false;

    public GameObject wumpaFruitPrefab;

    //public Text youDiedText;

    


    private GameObject gameController;
    private GameController gameControllerScript;

    private bool isDead = false;

    private bool levelCompleted = false;

    //Referenciamos el texto para la animación de Muerte
    public GameObject youDiedTextObject;
    private Animator textAnimator;
    private Text youDiedText;

    //Referenciamos el fondo negro para la animación de Muerte
    public GameObject youDiedImageObject;
    private Animator youDiedImageAnimator;
    private Image youDiedImage;


    //Referenciamos el texto para la animación de Muerte
    public GameObject youWonTextObject;
    private Animator textWonAnimator;
    private Text youWonText;

    //Referenciamos el fondo negro para la animación de Muerte
    public GameObject youWonImageObject;
    private Animator youWonImageAnimator;
    private Image youWonImage;

    //Boton de regreso al menu principal

    public GameObject regresarMenuPrincipal;
    public GameObject canvas2D_UI;




    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();

        _characterController = GetComponent<CharacterController>();

        gameController = GameObject.Find("GameController");

        //uiCanvas = GameObject.Find("UI_Canvas_2D");
        gameControllerScript = gameController.GetComponent<GameController>();

        DontDestroyOnLoad(gameController);

        //Inicializamos los objetos para la animación de muerte
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

        //Inicializamos los objetos para la animación de GANAR
        if (youWonTextObject != null)
        {
            textWonAnimator = youWonTextObject.GetComponent<Animator>();
            textWonAnimator.enabled = false;
        }

        if (youWonTextObject != null)
        {
            youWonText = youWonTextObject.GetComponent<Text>();
            youWonText.enabled = false;
        }

        if (youWonImageObject != null)
        {
            youWonImageAnimator = youWonImageObject.GetComponent<Animator>();
            youWonImageAnimator.enabled = false;
        }

        if (youWonImageObject != null)
        {
            youWonImage = youWonImageObject.GetComponent<Image>();
            youWonImage.enabled = false;
        }

        if (regresarMenuPrincipal != null)
            regresarMenuPrincipal.SetActive(false);

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

        if(_characterController.enabled == true)
            _characterController.Move(_moveDir * Time.deltaTime);

        if(isDead == true)
        {
            isDead = false;
            Invoke("playYouDied", 2f);
            
        }

        if (levelCompleted == true)
        {
            isDead = false;
            Invoke("LevelCompleted", 2f);

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
                Debug.Log("Collision con: " + hit.gameObject.name.ToString());
            }
            

        }

        //Detectamos si el objeto tiene la etiqueta Crush, en caso de que si se muere crash aplastado
        if (hit.gameObject.tag == "Crush2" && whoaPlayed == false)
        {

            if (whoaClip != null)
            {
                whoaPlayed = true;
                _animator.SetBool("isCrushed2", true);
                AudioManager.Instance.Play(whoaClip, transform);
                _characterController.enabled = false;
                isDead = true;
                Debug.Log("Collision con: " + hit.gameObject.name.ToString());
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
                Debug.Log("Collision con: " + hit.gameObject.name.ToString());
            }

            
 
        }

        //Verificamos si el objeto es la plataforma donde termina el nivel
        if (hit.gameObject.tag == "PlataformaGanar" && whoaPlayed == false)
        {
            if (whoaClip != null)
            {
                whoaPlayed = true;
                //_animator.SetBool("isDrowning", true);
                AudioManager.Instance.Play(whoaClip, transform);
                AudioManager.Instance.Play(tadaClip, transform);
                _characterController.enabled = true;
                levelCompleted = true;
                Debug.Log("Se ha llegado a la plataforma");
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
                Debug.Log("Collision con: " + hit.gameObject.name.ToString());

            }
            


        }

        //Verificamos si el objeto es el NitroCrate, en caso de que sí y esté girando, se destruye
  
        if (hit.gameObject.tag == "NitroCrate" && isSpinning == true)
        {


            if (standarCrateBreakClip != null)
            {
                whoaPlayed = true;
                _animator.SetBool("isExploded", true);
                AudioManager.Instance.Play(nitroCrateExplosionClip, transform, 1f);
                _characterController.enabled = false;
                isDead = true;
                Destroy(hit.gameObject);
                Debug.Log("Collision con: " + hit.gameObject.name.ToString());

            }



        }

        //Verificamos si el objeto es el NitroCrate, en caso de que sí y esté girando, se destruye

        if (hit.gameObject.tag == "ExtraLifeCrate" && isSpinning == true)
        {


            //whoaPlayed = true;
            //_animator.SetBool("isExploded", true);
            //AudioManager.Instance.Play(nitroCrateExplosionClip, transform, 1f);
            //_characterController.enabled = false;
            //isDead = true;
            //Destroy(hit.gameObject);
            Destroy(hit.gameObject);

            gameControllerScript.extraLifeGrabbed = true;
            Debug.Log("Collision con: " + hit.gameObject.name.ToString());




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
                Debug.Log("Collision con: " + hit.gameObject.name.ToString());

            }
        }
    }


    //Esto no funciona porque no posee RigiBody y sin esto no funciona los colliders

    private void OnCollisionEnter(Collision collision)
    {
        //Detectamos si el objeto tiene la etiqueta Crush, en caso de que si se muere crash aplastado
        if (collision.gameObject.tag == "Crush" && whoaPlayed == false)
        {

            if (whoaClip != null)
            {
                whoaPlayed = true;
                _animator.SetBool("isCrushed", true);
                AudioManager.Instance.Play(whoaClip, transform);
                _characterController.enabled = false;
                isDead = true;
                Debug.Log("Collision ENTER: " + collision.gameObject.name.ToString());
            }


        }

        //Detectamos si el objeto tiene la etiqueta Crush, en caso de que si se muere crash aplastado
        if (collision.gameObject.tag == "Crush2" && whoaPlayed == false)
        {

            if (whoaClip != null)
            {
                whoaPlayed = true;
                _animator.SetBool("isCrushed2", true);
                AudioManager.Instance.Play(whoaClip, transform);
                _characterController.enabled = false;
                isDead = true;
                Debug.Log("Collision ENTER: " + collision.gameObject.name.ToString());
            }


        }

        //Verificamos si el objeto es agua, si es agua se muere Crash ahogado
        if (collision.gameObject.tag == "Water" && whoaPlayed == false)
        {
            if (whoaClip != null)
            {
                whoaPlayed = true;
                _animator.SetBool("isDrowning", true);
                AudioManager.Instance.Play(whoaClip, transform);
                _characterController.enabled = false;
                isDead = true;
                Debug.Log("Collision ENTER: " + collision.gameObject.name.ToString());
            }



        }

        //Verificamos si el objeto es el StandardCrate, en caso de que sí y esté girando, se destruye
        //Así mismo, instanciamos un Prefab de WumpaFruit
        if (collision.gameObject.tag == "StandardCrate" && isSpinning == true)
        {


            if (standarCrateBreakClip != null)
            {
                AudioManager.Instance.Play(standarCrateBreakClip, transform, 1f);

                Instantiate(wumpaFruitPrefab, collision.gameObject.transform.position, Quaternion.identity);

                Destroy(collision.gameObject);
                Debug.Log("Collision ENTER: " + collision.gameObject.name.ToString());


            }


        }

        //Verificamos si el objeto es el NitroCrate, en caso de que sí y esté girando, se destruye
  
        if (collision.gameObject.tag == "NitroCrate" && isSpinning == true)
        {


            if (standarCrateBreakClip != null)
            {
                whoaPlayed = true;
                _animator.SetBool("isExploded", true);
                AudioManager.Instance.Play(nitroCrateExplosionClip, transform, 1f);
                _characterController.enabled = false;
                isDead = true;
                Destroy(collision.gameObject);
                Debug.Log("Collision con: " + collision.gameObject.name.ToString());

            }



        }

        //Verificamos si el objeto es el WumpaFruit, en caso de que si se hace un Random Para saber cuántos WumpaFruits
        //Recogió Crash
        if (collision.gameObject.tag == "WumpaFruit")
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

                Destroy(collision.gameObject);
                Debug.Log("Collision ENTER: " + collision.gameObject.name.ToString());


            }
        }
    }
    //Función que carga la escena 1
    private void ReturnToMainMenu()
    {
        gameControllerScript.firstGame = true;
        Destroy(gameController);
        SceneManager.LoadScene("Menu_Principal");
        
    }
    //Función que se llama al ganar/ pasar el nivel
    private void LevelCompleted()
    {
        AudioManager.Instance.Play(winninFanfareClip, transform, 1f, 1f);
        if (textWonAnimator != null)
        {
            textWonAnimator.enabled = true;
        }

        if (youWonImageAnimator != null)
        {
            youWonImageAnimator.enabled = true;
        }

        _characterController.enabled = false;

        //Llamos a la función con la que se termina el juego
        Invoke("ReturnToMainMenu", 5f);
    }

    //Funcion que se llama al morir
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
        if(gameControllerScript.livesCounter>=0)
            Invoke("RestartLevel", 3f);
        else
            Invoke("GameOver", 3f);

    }

    private void GameOver()
    {
        gameControllerScript.firstGame = true;
        AudioManager.Instance.Play(gameOverClip, transform, 1f, 1f);
        Invoke("GameOver2", 3f);
    }

    private void GameOver2()
    {
        Debug.Log("GameOver2");
        Debug.Log("Active state: " + regresarMenuPrincipal.activeSelf);
        //AudioManager.Instance.Play(gameOverClip, transform, 1f, 1f);
        //transform.Find("ButtonReturnMainMenu").gameObject.SetActive(true);
        
        if (gameController!=null)
            Destroy(gameController);
        if (canvas2D_UI != null)
            Destroy(canvas2D_UI);

        regresarMenuPrincipal.SetActive(true);
        Debug.Log("Active state: " + regresarMenuPrincipal.activeSelf);
    }

    //Funcion que se llama al reiniciar el nivel
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
