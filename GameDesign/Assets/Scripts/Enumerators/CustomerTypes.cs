using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
**/
public enum CustomerTypes
{
    Normal=0,
    AntiMasker,
    Runner,
    Dinosaur,//jock?

}

public class EnumNamedArrayAttribute : PropertyAttribute
{
    public string[] names;
    public EnumNamedArrayAttribute(System.Type names_enum_type)
    {
        this.names = System.Enum.GetNames(names_enum_type);
    }
}

