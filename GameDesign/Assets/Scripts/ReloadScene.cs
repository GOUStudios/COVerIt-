using UnityEngine;

public class ReloadScene : MonoBehaviour
{
    public void loadLastScene()
    {
        Debug.Log("Loading last Scene " + ScenesManager.lastLoadedLevel);
        ScenesManager.Instance.SceneChangerLevel(ScenesManager.lastLoadedLevel);
    }
}
