using com.fscigliano.ReflectionExtensions.Editor;
using UnityEngine;
using UnityEditor;

namespace com.fscigliano.PropertyDrawersCollection.Editor
{
    /// <summary>
    /// Creation Date:   29/02/2020 16:44:31
    /// Product Name:    Property Drawers Collection
    /// Developers:      Franco Scigliano
    /// Description:     Shows a button that set the icon for the script file as the one of the parameter value.
    ///                  Useful to fix inherited classes icons.
    /// </summary>
    [CustomPropertyDrawer(typeof(FixIconAttribute), true)]
    public class FixIconPropertyDrawer : CastedAttributePropertyDrawer<FixIconAttribute>
    {
        #region Properties, Consts and Statics
        const float buttonHeight = 16;
        const float buttonSpace = 4;
        const float buttonWidth = 60;
        #endregion

        #region Editor Variables
        private Rect pRect;
        private Rect bRect;
        #endregion
        #region Methods
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            FixIconAttribute cAttribute = attribute as FixIconAttribute;
            if (!cAttribute.hideProperty)
            {
                pRect = new Rect(position.x, position.y, position.width, position.height - buttonHeight - buttonSpace);
                bRect = new Rect(EditorGUIUtility.currentViewWidth - buttonWidth - 5, position.y + pRect.height + buttonSpace, buttonWidth, buttonHeight);
                EditorGUI.PropertyField(pRect, property);
            }
            else
            {
                bRect = new Rect(EditorGUIUtility.currentViewWidth - buttonWidth - 5, position.y, buttonWidth, buttonHeight);
            }
            if (GUI.Button(bRect,"Fix Icon"))
            {
                string scriptName = property.serializedObject.targetObject.GetType().Name;
                string iconFileName = cAttribute.IconFileName;
                ReflectionExtensions.Editor.ReflectionExtensions.SetIcon(scriptName, iconFileName);
            }
            EditorGUI.EndProperty();
        }
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            FixIconAttribute cAttribute = attribute as FixIconAttribute;
            if (cAttribute.hideProperty)
            {
                return buttonHeight;
            }
            return base.GetPropertyHeight(property, label) + buttonHeight+ buttonSpace;
        }
        #endregion
    }
}