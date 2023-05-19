using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectUtils
{
    public static MonoBehaviour GetObjectWithInterface<T>(GameObject gameObject)
    {
        MonoBehaviour[] list = gameObject.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour mb in list)
        {
            if (mb is T)
            {
                return mb;
            }
        }
        return null;
    }
}
