using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace com.fscigliano.PropertyDrawersCollection.Editor
{
    /// <summary>
    /// Creation Date:   24/02/2020 23:09:12
    /// Product Name:    Property Drawers Collection
    /// Developers:      Franco Scigliano
    /// Description:     Draws a TextArea with a string that can be a fixed text, the content of a property, or
    ///                  the content of the string that the attribute is decorating.
    ///                  Valid for values of type string.
    /// Changelog:      
    /// </summary>
    [CustomPropertyDrawer(typeof(DescriptionAttribute), true)]
    public class DescriptionAttributeDrawer : CastedAttributePropertyDrawer<DescriptionAttribute>
    {
        #region Classes, Structs and Enums

        protected enum ResultType
        {
            NOT_SET = 0, USE_STR_VALUE = 1, USE_PARAMETER_STR = 2, USE_PARAMETER_VALUE = 3
        }
        #endregion

        #region Properties, Consts and Statics
        protected override List<SerializedPropertyType> validTypes { get { return new List<SerializedPropertyType> (new SerializedPropertyType[] { SerializedPropertyType.String }); } }
        #endregion

        #region Methods
        protected override void DoOnGUI (Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            GUIStyle myStyle = new GUIStyle (EditorStyles.textArea);
            ResultType useStrValue = ResultType.NOT_SET;
            string desc = string.Empty;
            if (!cAttribute.hasParameter)
            {
                useStrValue = ResultType.USE_STR_VALUE;
            }
            else
            {
                PropertyInfo p = ReflectionExtensions.Editor.ReflectionExtensions.GetProperty(cAttribute.classDescription, property.serializedObject.targetObject);
                if (p == null)
                {
                    desc = cAttribute.classDescription;
                    useStrValue = ResultType.USE_PARAMETER_STR;
                }
                else
                {
                    desc = ReflectionExtensions.Editor.ReflectionExtensions.GetFromProp(p, property.serializedObject.targetObject);
                    useStrValue = desc == null ? ResultType.USE_STR_VALUE : ResultType.USE_PARAMETER_VALUE;
                }
            }
            if (useStrValue == ResultType.USE_STR_VALUE)
            {
                property.stringValue = EditorGUI.TextArea(position, property.stringValue, myStyle);
            }
            else
            {
                bool oldEnabled = GUI.enabled;
                GUI.enabled = false;
                EditorGUI.TextArea(position, desc, myStyle);
                GUI.enabled = oldEnabled;
            }
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            ResultType useStrValue = ResultType.NOT_SET;
            string desc = string.Empty;
            if (!cAttribute.hasParameter)
            {
                useStrValue = ResultType.USE_STR_VALUE;
            }
            else
            {
                PropertyInfo p = ReflectionExtensions.Editor.ReflectionExtensions.GetProperty(cAttribute.classDescription, property.serializedObject.targetObject);
                if (p == null)
                {
                    desc = cAttribute.classDescription;
                    useStrValue = ResultType.USE_PARAMETER_STR;
                }
                else
                {
                    desc = ReflectionExtensions.Editor.ReflectionExtensions.GetFromProp(p, property.serializedObject.targetObject);
                    useStrValue = desc == null ? ResultType.USE_STR_VALUE : ResultType.USE_PARAMETER_VALUE;
                }
            }

            if (useStrValue == ResultType.USE_STR_VALUE)
            {
                desc = property.stringValue;
            }
            GUIContent guiContent = new GUIContent (desc);
            GUIStyle myStyle = new GUIStyle (EditorStyles.textArea);
            float textHeight = myStyle.CalcHeight (guiContent, EditorGUIUtility.currentViewWidth);
            return Mathf.Max(50, textHeight);
        }
        #endregion
    }
}
