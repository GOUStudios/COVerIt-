using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarsMaterialSelector : MonoBehaviour
{
    public Material StarGained;

    [SerializeField] private GameObject star1;
    [SerializeField] private GameObject star2;
    [SerializeField] private GameObject star3;

    private void Awake()
    {
        if (CheckCondition() != 0)
        {
            SetMaterial(CheckCondition());
        }
        
    }

    //this function should check how many stars the player has previously earned for the level
    private int CheckCondition()
    {
       

        return 1;
    }


    //Assign the right material in order to the stars earned previously for this level
    private void SetMaterial(int starsNumbers)
    {
        switch(starsNumbers)
        {
            case 1:
                
                star1.GetComponent<Renderer>().material = StarGained;

                break;

            case 2:

                star1.GetComponent<Renderer>().material = StarGained;

                star2.GetComponent<Renderer>().material = StarGained;

                break;

            case 3:

                star1.GetComponent<Renderer>().material = StarGained;

                star2.GetComponent<Renderer>().material = StarGained;

                star3.GetComponent<Renderer>().material = StarGained;
                
                break;

            default:
                
                Debug.LogWarning("ERROR In Stars Materials");
                
                break;

        }
    }
}
