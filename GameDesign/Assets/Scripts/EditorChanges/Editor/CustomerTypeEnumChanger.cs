using UnityEngine;
using UnityEditor;

using UnityEngine.UIElements;
using UnityEditor.UIElements;
[CustomPropertyDrawer(typeof(EnumNamedArrayAttribute))]
public class DrawerEnumNamedArray : PropertyDrawer
{
    SerializedProperty array;
    SerializedProperty content;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property);
    }
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {

        EditorGUI.BeginProperty(position, label , property);

        EnumNamedArrayAttribute enumNames = (EnumNamedArrayAttribute)attribute ;
        string path = property.propertyPath;

        if (array == null) { 
            array = property.serializedObject.FindProperty(path.Substring(0,path.LastIndexOf('.')));
            if (array == null) {
                EditorGUI.LabelField(position, "not using an EnumNamedArrayAttribute as array");
                return;
            }
            if (array.arraySize != enumNames.names.Length) { 
                array.arraySize = enumNames.names.Length;
            }
        }
        

        //propertyPath returns something like component_hp_max.Array.data[4]
        //so get the index from there
        int index = System.Convert.ToInt32(path.Substring(path.IndexOf("[") + 1).Replace("]", ""));
        //change the label
        label.text = enumNames.names[index];
        //draw field
        EditorGUI.PropertyField(position, property, label, true);

        EditorGUI.EndProperty();


    }
}