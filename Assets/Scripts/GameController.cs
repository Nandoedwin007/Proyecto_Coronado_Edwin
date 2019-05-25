using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update

    private bool firstGame = true;
    private int livesCounter = 3;
    private int wumpaCounter = 0;

    public Text livesCounterText;
    public Text wumpaCounterText;
    void Start()
    {
        if (firstGame == true)
        {
            firstGame = false;
            livesCounter = 3;
            wumpaCounter = 0;

            wumpaCounterText.text = wumpaCounter.ToString();
            livesCounterText.text = livesCounter.ToString();
        }
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
    }
}
