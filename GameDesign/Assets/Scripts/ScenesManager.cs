using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScenesManager : MonoBehaviour
{
    private static ScenesManager instance;

    //levelIsReady is a flag to control the loading progress of the level scene
    public static bool levelIsReady = false;
    
    private void Awake()
    {
        if (instance == null)
        {
            // Keep this object alive between all the scenes
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            // if exists another instance of SceneManager, destroy this one
            Destroy(gameObject);
        }
    }

    
    public void SceneChanger(string sceneName)
    {

        // if (!CheckSceneExists(sceneName)) return;

        if (SceneManager.GetActiveScene().name == sceneName)
        {
            Debug.LogWarning("This Scene is already uploaded");
            return;
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    //Scene changer that use a fade black to change
    public void SceneChangerWFade(string sceneName)
    {
        if (levelIsReady) levelIsReady = false;

        StartCoroutine(FadeOutAndLoadScene(sceneName));
    }



    //Scene changer that use a fade black to change
    public void SceneChangerLevel(string sceneName)
    {
        if (levelIsReady) levelIsReady = false;
        
        StartCoroutine(LoadSceneWaiting(sceneName));
    }

    IEnumerator LoadSceneWaiting(string sceneName)
    {
        CanvasGroup fadeCanvasGroup = GetComponentInChildren<CanvasGroup>();

        float fadeSpeed = 2f;

        while (fadeCanvasGroup.alpha < 1f)
        {
            FadeToBlack(fadeCanvasGroup, fadeSpeed);
            yield return null;
        }

        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);

       
        yield return new WaitWhile(() => levelIsReady == false);

        while (fadeCanvasGroup.alpha > 0f)
        {
            FadeToTransparent(fadeCanvasGroup, fadeSpeed);
            yield return null;
        }
    }
    
    
    IEnumerator FadeOutAndLoadScene(string sceneName)
    {
        CanvasGroup fadeCanvasGroup = GetComponentInChildren<CanvasGroup>();

        float fadeSpeed = 2f;

        while (fadeCanvasGroup.alpha < 1f)
        {
            FadeToBlack(fadeCanvasGroup, fadeSpeed);
            yield return null;
        }

        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);

       
        yield return new WaitUntil(() => levelIsReady == true);

        while (fadeCanvasGroup.alpha > 0f)
        {
            FadeToTransparent(fadeCanvasGroup, fadeSpeed);
            yield return null;
        }
    }

    void FadeToTransparent(CanvasGroup fadeCanvasGroup, float fadeSpeed)
    {
        fadeCanvasGroup.alpha = Mathf.Lerp(fadeCanvasGroup.alpha, 0f, fadeSpeed * Time.deltaTime);
        if (fadeCanvasGroup.alpha <= 0.1f) fadeCanvasGroup.alpha = 0f;
    }


    void FadeToBlack(CanvasGroup fadeCanvasGroup,float fadeSpeed)
    {
        
        fadeCanvasGroup.alpha = Mathf.Lerp(fadeCanvasGroup.alpha, 1f, fadeSpeed * Time.deltaTime);
        if(fadeCanvasGroup.alpha >= 0.9f) fadeCanvasGroup.alpha = 1f;
    }




    private bool CheckSceneExists(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            if (sceneName == SceneManager.GetSceneAt(i).name)
            {
               
                return true;
            }
        }

        Debug.LogWarning("This Scene doesn't exist!!!");
        return false;
    }


    //Function to quit 
    public void QuitApplication()
    {
        Application.Quit();
    }


}
