using UnityEngine;

public class ReloadScene : MonoBehaviour
{
    public void loadLastScene()
    {
        Time.timeScale=1;
        Debug.Log("Loading last Scene " + ScenesManager.lastLoadedLevel);
        ScenesManager.Instance.SceneChangerLevel(ScenesManager.lastLoadedLevel);
    }
}
