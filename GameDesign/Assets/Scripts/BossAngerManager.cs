using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAngerManager : MonoBehaviour
{
    [SerializeField] public int _maxNumAngry = 10; //To be defined on each level
    [SerializeField] public int _maxNumGameOver = 6; //To be defined on each level

    private static BossAngerManager instance;

    public int angryCounter;
    public int gameOverCounter;
    public bool isAngry;

    public static BossAngerManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<BossAngerManager>();
                if (instance == null)
                {
                    GameObject bossObject = new GameObject(typeof(BossAngerManager).Name);
                    instance = bossObject.AddComponent<BossAngerManager>();
                    instance.angryCounter = 0;
                    instance.gameOverCounter = 0;
                    instance.isAngry = false;
                }
            }

            return instance;
        }
    }

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void triggerEvent_MissClicked()
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

    public void triggerEvent_CorrectlyClicked()
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
            angryCounter = 0;
            gameOverCounter = 0;
            isAngry = false;
        }

        //Use to pass from angry to normal state
        if (gameOverCounter == 0 && isAngry)
        {
            isAngry = false;
            angryCounter -= 1;
        }
    }
}
