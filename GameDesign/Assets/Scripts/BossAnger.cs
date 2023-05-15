using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnger : MonoBehaviour
{
    private static int MAX_NUMBER_ANGRY = 10; //To be defined on each level
    private static int MAX_NUMBER_GAME_OVER = 6; //To be defined on each level

    public static int angryCounter;
    public static int gameOverCounter;
    public static bool isAngry;
    public static bool coveredIt; // Variable to know whether the click made by player was a mistake or not
    
    // Start is called before the first frame update
    void Start()
    {
        angryCounter = 0;
        gameOverCounter = 0;
        isAngry = false;
        coveredIt = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!coveredIt) {
                if (isAngry == true)
                {
                    gameOverCounter += 1;
                    Debug.Log("Fail to game over number " + gameOverCounter);
                }
                else
                {
                    angryCounter += 1;
                    Debug.Log("Fail to be angry number " + angryCounter);
                }
            }
            else
            {
                if (isAngry && gameOverCounter > 0)
                {
                    gameOverCounter -= 1;
                    Debug.Log("Good shot game over number " + gameOverCounter);
                }
                else
                {
                    if(angryCounter > 0)
                    {
                        angryCounter -= 1;
                        Debug.Log("Good shot to be angry number " + angryCounter);
                    }
                }
            }
            coveredIt = false;
        }

        if(angryCounter >= MAX_NUMBER_ANGRY && !isAngry)
        {
            isAngry = true;
            gameOverCounter = 1;
            Debug.Log("Boss is angry >:(");
        }

        if(gameOverCounter >= MAX_NUMBER_GAME_OVER)
        {
            Debug.Log("GAME OVER");
            angryCounter = 0;
            gameOverCounter = 0;
            isAngry = false;
        }

        //Use to pass from angry to normal state
        if(gameOverCounter == 0 && isAngry)
        {
           isAngry = false;
           angryCounter -= 1;
        }
    }
}
