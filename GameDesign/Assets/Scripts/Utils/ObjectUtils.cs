using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectUtils
{
    public static MonoBehaviour GetObjectWithInterface<T>(GameObject gameObject)
    {
        if (gameObject == null) return null;
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

    public static string DictionaryToString<T,K>(Dictionary<T,K> dict)
    {
        string s = "{";
        foreach(var (k, v) in dict)
        {
            s += $"{k} -> {v}, ";
        }
        s += "}";
        return s;
    }

    public static string ArrayToString<T>(T[] values)
    {
        string s = "[";
        foreach(var v in values)
        {
            s += $"{v}, ";
        }
        s += "]";
        return s;
    }
}
