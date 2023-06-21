using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class EnumDataContainer<TValue, TEnum> where TEnum :Enum
{
    [SerializeField] TValue[] content = null;
    [SerializeField] TEnum enumType;

    public TValue this[int i]{
        get { return content[i]; }
        set { content[i] = value; }
    }
    
    public int Length{ get { return content.Length; } }
}
