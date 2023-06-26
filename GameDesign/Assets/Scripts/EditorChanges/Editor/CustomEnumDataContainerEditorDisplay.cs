using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(EnumDataContainer<,>))]
public class CustomEnumDataContainerEditorDisplay : PropertyDrawer
{

    private const float FOLDOUT_HEIGHT = 16f;
    private SerializedProperty content;
    private SerializedProperty enumType;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {

        if (content == null)
        {
            content = property.FindPropertyRelative("content");
        }
        if (enumType == null)
        {
            enumType = property.FindPropertyRelative("enumType");

        }
        float height = FOLDOUT_HEIGHT;

        if (property.isExpanded)
        {
            if (content.arraySize != enumType.enumNames.Length)
                content.arraySize = enumType.enumNames.Length;
            for (int i = 0; i < content.arraySize; i++)
            {
                height += EditorGUI.GetPropertyHeight(content.GetArrayElementAtIndex(i));
            }
        }

        return height;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {

        EditorGUI.BeginProperty(position, label, property);

        Rect foldoutRect = new Rect(position.x, position.y, position.width, FOLDOUT_HEIGHT);
        property.isExpanded = EditorGUI.Foldout(foldoutRect, property.isExpanded, label);

        if (property.isExpanded)
        {

            EditorGUI.indentLevel++;
            float addY = FOLDOUT_HEIGHT;
            for (int i = 0; i < content.arraySize; i++)
            {
                Rect rect = new Rect(position.x, position.y + addY, position.width, EditorGUI.GetPropertyHeight(content.GetArrayElementAtIndex(i)));
                addY += rect.height;
                EditorGUI.PropertyField(rect, content.GetArrayElementAtIndex(i), new GUIContent(enumType.enumNames[i]), true);
            }
            EditorGUI.indentLevel--;
        }

        EditorGUI.EndProperty();


    }
}
