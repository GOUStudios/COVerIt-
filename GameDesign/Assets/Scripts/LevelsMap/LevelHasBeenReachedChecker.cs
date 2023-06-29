using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHasBeenReachedChecker : MonoBehaviour
{
    [SerializeField] int LevelNumber;
    // Start is called before the first frame update
    void Start()
    {
        if (LevelNumber > PlayerPrefs.GetInt("levelReached", 1))
        {
            this.gameObject.SetActive(false);
        }
        enabled = false;
    }

}
