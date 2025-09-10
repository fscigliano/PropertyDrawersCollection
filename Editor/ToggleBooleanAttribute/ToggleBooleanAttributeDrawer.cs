using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace com.fscigliano.PropertyDrawersCollection.Editor
{
    /// <summary>
    /// Creation Date:   01/02/2020 22:51:22
    /// Product Name:    Property Drawers Collection
    /// Developers:      Franco Scigliano
    /// Description:
    /// </summary>
    [CustomPropertyDrawer(typeof(ToggleBooleanAttribute))]
    public class ToggleBooleanAttributeDrawer : CastedAttributePropertyDrawer<ToggleBooleanAttribute>
    {
        protected override List<SerializedPropertyType> validTypes
        {
            get
            {
                return new List<SerializedPropertyType>(new SerializedPropertyType[]
                    { SerializedPropertyType.Boolean });
            }
        }

        protected override void DoOnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var centeredStyle = GUI.skin.GetStyle("Label");
            centeredStyle.alignment = TextAnchor.UpperCenter;
            EditorGUI.BeginProperty(position, label, property);
            property.boolValue = EditorGUI.Toggle(position, property.boolValue, "Button");
            EditorGUI.LabelField(position, label, centeredStyle);
            EditorGUI.EndProperty();
        }
    }
}