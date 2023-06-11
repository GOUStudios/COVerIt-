using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class ChangeSceneRequest : MonoBehaviour
{
    private ScenesManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<ScenesManager>();

        if(manager == null) {
            Debug.LogWarning("No Scenes Manager found");
        }    
    }


    public void RequestLevel(string nameScene)
    {
        if(manager == null)
        {
            Debug.LogWarning("No Scenes Manager found");
            return;
        }
        
        if(nameScene == null)
        {
            Debug.LogWarning("No NameScene Specified" + nameScene);
            return;
        }

        Debug.Log("NameScene: " + nameScene);
        manager.SceneChangerLevel(nameScene);
    }
    
    
    public void Request(string nameScene)
    {
        if(manager == null)
        {
            Debug.LogWarning("No Scenes Manager found");
            return;
        }
        
        if(nameScene == null)
        {
            Debug.LogWarning("No NameScene Specified" + nameScene);
            return;
        }

        Debug.Log("NameScene: " + nameScene);
        manager.SceneChanger(nameScene);
    }

    
}
