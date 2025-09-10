using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.IO;

namespace com.fscigliano.PropertyDrawersCollection.Editor
{
    /// <summary>
    /// Creation Date:   01/02/2020 22:51:22
    /// Product Name:    Property Drawers Collection
    /// Developers:      Franco Scigliano
    /// Description:	 
    /// </summary>
    [CustomPropertyDrawer(typeof(ResourceReferenceAttribute), true)]
    public class ResourceReferenceAttributeDrawer : CastedAttributePropertyDrawer<ResourceReferenceAttribute>
    {
        public override bool CanCacheInspectorGUI(SerializedProperty property)
        {
            return true;
        }

        protected override List<SerializedPropertyType> validTypes { get { return new List<SerializedPropertyType> (new SerializedPropertyType[] { SerializedPropertyType.String }); } }

        protected override void DoOnGUI (Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            Color oldcolor = GUI.backgroundColor;

            UnityEngine.Object result = null;
            if (string.IsNullOrEmpty(property.stringValue))
            {
                if (!string.IsNullOrEmpty(cAttribute.filter))
                {
                    label.tooltip = string.Format("not assigned. Filter {0}", cAttribute.filter);
                }
                else
                {
                    label.tooltip = "not assigned";
                }
            }
            else
            {
                result = Resources.Load<UnityEngine.Object>(property.stringValue);
                if (result == null)
                {
                    GUI.backgroundColor = Color.red;
                    label.tooltip = string.Format("invalid path: {0}", property.stringValue);
                }
                else
                {
                    label.tooltip = string.Format("path: {0}", property.stringValue);
                }
            }
            
            EditorGUI.BeginChangeCheck();
            result = EditorGUI.ObjectField(position, label, result, typeof(UnityEngine.Object), false);
            if (EditorGUI.EndChangeCheck())
            {
                if (result == null)
                {
                    property.stringValue = null;
                }
                else
                {
                    string fullPath = AssetDatabase.GetAssetPath(result);
                    string resourcePath = GetResourcePath(fullPath);
                    // ReSharper disable once ReplaceWithSingleAssignment.True
                    bool valid = true;
                    if (!string.IsNullOrEmpty(cAttribute.filter) && !resourcePath.Contains(cAttribute.filter))
                    {
                        valid = false;
                    }
                    
                    if (cAttribute.type != null && cAttribute.type != result.GetType())
                    {
                        valid = false;
                    }
                    if (valid)
                        property.stringValue = resourcePath;
                }
            }

            GUI.backgroundColor = oldcolor;
            EditorGUI.EndProperty();
        }

        private string GetResourcePath(string fullPath)
        {
            if (fullPath.Contains("Resources/"))
            {
                string result = Path.ChangeExtension(fullPath, null);
                result = result.Substring(result.LastIndexOf("Resources/") + 10);
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}