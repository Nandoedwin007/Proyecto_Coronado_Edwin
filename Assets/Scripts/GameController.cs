using ChrisTutorials.Persistent;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update

    public bool firstGame = true;
    public int livesCounter = 3;
    public int wumpaCounter = 0;

    public bool extraLifeGrabbed = false;

    

    public Text livesCounterText;
    public Text wumpaCounterText;

    public AudioClip extraLifeClip;
    void Start()
    {
        if (firstGame == true)
        {
            firstGame = false;
            livesCounter = 3;
            wumpaCounter = 0;

            if(wumpaCounterText != null)
                wumpaCounterText.text = wumpaCounter.ToString();
            if (livesCounterText != null)
                livesCounterText.text = livesCounter.ToString();
        }

        //DontDestroyOnLoad(livesCounterText);
        //DontDestroyOnLoad(wumpaCounterText);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (firstGame == true)
        {
            firstGame = false;
            livesCounter = 3;
            wumpaCounter = 0;

            wumpaCounterText.text = wumpaCounter.ToString();
            livesCounterText.text = livesCounter.ToString();
        }


        if (extraLifeGrabbed == true)
        {
            extraLifeGrabbed = false;
            livesCounter += 1;
            AudioManager.Instance.Play(extraLifeClip, transform, 1f);


            Debug.Log("Extra Life");
        }
        if (wumpaCounter >= 100)
        {
            wumpaCounter = 0;
            livesCounter += 1;

            if (extraLifeClip != null)
            {
     
                AudioManager.Instance.Play(extraLifeClip, transform, 1f);


                Debug.Log("Extra Life");

            }
        }

        if (wumpaCounterText != null)
            wumpaCounterText.text = wumpaCounter.ToString();
        if (livesCounterText != null)
            livesCounterText.text = livesCounter.ToString();
    }
}
