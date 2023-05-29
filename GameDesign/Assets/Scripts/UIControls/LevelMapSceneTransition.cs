using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;


//This Script loads the LevelMap Scene at the end of the clip video
public class LevelMapSceneTransition : MonoBehaviour
{
    private VideoPlayer _clip;

    // Start is called before the first frame update
    void Start()
    {
        _clip= GetComponent<VideoPlayer>();

        _clip.loopPointReached += OnVideoEnd;
    }

    //Invoked when the clip reach the end
    private void OnVideoEnd(VideoPlayer player)
    {
        SceneManager.LoadScene("LevelMap");
    }
}
