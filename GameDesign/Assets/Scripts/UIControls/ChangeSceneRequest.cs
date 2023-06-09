using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSceneRequest : MonoBehaviour
{
    private ScenesManager manager;

    public string nameScene;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<ScenesManager>();
        if(manager == null) {
            Debug.LogWarning("No Scenes Manager found");
        }    
    }


    public void Request(string nameScene)
    {
        if(manager == null)
        {
            Debug.LogWarning("No Scenes Manager found");
            return;
        }
        manager.SceneChangerWFade(nameScene);
    }

    
}
