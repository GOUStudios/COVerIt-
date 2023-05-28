using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAngerManager
{

    private static BossAngerManager _instance;

    public int _maxNumAngry = 10; //To be defined on each level
    public int _maxNumGameOver = 6; //To be defined on each level

    public int angryCounter = 0;
    public int gameOverCounter = 0;
    public bool isAngry;

    public float angerPercent
    {
        get
        {
            return (float)(angryCounter + gameOverCounter) / (float)(_maxNumAngry + _maxNumGameOver);
        }
    }
    public float angerThreshhold
    {
        get
        {
            return (float)(_maxNumAngry) / (float)(_maxNumAngry + _maxNumGameOver);
        }
    }

    public BossAngerManager()
    {
        angryCounter = 0;
        gameOverCounter = 0;
        isAngry = false;
    }

    public static BossAngerManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new BossAngerManager();
                Debug.Log($"{_instance}");
            }
            return _instance;
        }
    }

    public void TriggerEvent_MissClicked()
    {
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

        checkAnger();
    }

    public void TriggerEvent_CorrectlyClicked()
    {
        if (isAngry && gameOverCounter > 0)
        {
            gameOverCounter -= 1;
            Debug.Log("Good shot game over number " + gameOverCounter);
        }
        else
        {
            if (angryCounter > 0)
            {
                angryCounter -= 1;
                Debug.Log("Good shot to be angry number " + angryCounter);
            }
        }
        checkAnger();
    }

    public void checkAnger()
    {
        if (angryCounter >= _maxNumAngry && !isAngry)
        {
            isAngry = true;
            gameOverCounter = 1;
            Debug.Log("Boss is angry >:(");
        }

        if (gameOverCounter >= _maxNumGameOver)
        {
            Debug.Log("GAME OVER");
            //Dont think this reset is necessary
            /*angryCounter = 0;
            gameOverCounter = 0;
            isAngry = false;*/
        }

        //Use to pass from angry to normal state
        if (gameOverCounter == 0 && isAngry)
        {
            isAngry = false;
            angryCounter -= 1;
        }
    }
}
