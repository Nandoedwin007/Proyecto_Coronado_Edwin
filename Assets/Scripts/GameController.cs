using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update

    private bool firstGame = true;
    public int livesCounter = 3;
    public int wumpaCounter = 0;

    

    public Text livesCounterText;
    public Text wumpaCounterText;
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

        if (wumpaCounter == 100)
        {
            wumpaCounter = 0;
            livesCounter += 1;
        }

        if (wumpaCounterText != null)
            wumpaCounterText.text = wumpaCounter.ToString();
        if (livesCounterText != null)
            livesCounterText.text = livesCounter.ToString();
    }
}
