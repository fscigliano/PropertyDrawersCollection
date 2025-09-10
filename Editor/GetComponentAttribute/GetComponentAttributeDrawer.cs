using System;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace com.fscigliano.PropertyDrawersCollection.Editor
{
    /// <summary>
    /// Creation Date:   01/03/2020 16:50:41
    /// Product Name:    Property Drawers Collection
    /// Developers:      Franco Scigliano
    /// Description:     
    /// </summary>
	[CustomPropertyDrawer(typeof(GetComponentAttribute), true)]
	public class GetComponentAttributeDrawer : CastedAttributePropertyDrawer<GetComponentAttribute>
	{
        protected override List<SerializedPropertyType> validTypes { get { return new List<SerializedPropertyType> (new SerializedPropertyType[] { SerializedPropertyType.ObjectReference }); } }
        private const float buttonWidth = 40;
        private const float buttonSpace = 3;
        protected override void DoOnGUI (Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);
            
            Rect bRect = new Rect(position.x + position.width - buttonWidth, position.y, buttonWidth,
                EditorGUIUtility.singleLineHeight);
            position.width -= (buttonWidth + buttonSpace);

            Type t = ReflectionExtensions.Editor.ReflectionExtensions.GetPropertyType(property);
            EditorGUI.BeginChangeCheck();
            var o = EditorGUI.ObjectField(position, label, property.objectReferenceValue, t, true);
            if (EditorGUI.EndChangeCheck())
            {
                property.objectReferenceValue = o;    
            }
            if (GUI.Button(bRect, "Get"))
            {
                if (!property.serializedObject.isEditingMultipleObjects)
                {
                    Component gTarget = (property.serializedObject.targetObject as Component);
                    var c = gTarget.GetComponent(t);
                    if (c != null)
                    {
                        property.objectReferenceValue = c;
                    }    
                }
                else
                {
                    var obs = property.serializedObject.targetObjects;
                    foreach (var o2 in obs)
                    {
                        SerializedObject so = new SerializedObject(o2);
                        SerializedProperty prop2 = so.FindProperty(property.propertyPath);
                        Component gTarget = (o2 as Component);
                        var c = gTarget.GetComponent(t);
                        if (c != null)
                        {
                            prop2.objectReferenceValue = c;
                            so.ApplyModifiedProperties();
                        }    
                    }
                }
            }
            EditorGUI.EndProperty();
		}
    }
}