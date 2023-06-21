using System;
using UnityEngine;


public class EnumNamedArrayAttribute : PropertyAttribute
{
    public string[] names;
    public EnumNamedArrayAttribute(Type names_enum_type)
    {
        this.names = System.Enum.GetNames(names_enum_type);
    }
}
