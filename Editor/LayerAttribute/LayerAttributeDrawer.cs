using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace com.fscigliano.PropertyDrawersCollection.Editor
{
    /// <summary>
    /// Creation Date:   01/02/2020 22:46:50
    /// Product Name:    Property Drawers Collection
    /// Developers:      Franco Scigliano
    /// Description:	 original version available in https://github.com/uranuno/MyPropertyDrawers
    /// </summary>
	[CustomPropertyDrawer(typeof(LayerAttribute))]
	public class LayerDrawer : CastedAttributePropertyDrawer<LayerAttribute> {

		protected override List<SerializedPropertyType> validTypes { get { return new List<SerializedPropertyType> (new SerializedPropertyType[] { SerializedPropertyType.Integer }); } }

        protected override void DoOnGUI(Rect position, SerializedProperty prop, GUIContent label) {
			
			EditorGUI.BeginProperty (position, label, prop);
			
			prop.intValue = EditorGUI.LayerField(position, label, prop.intValue);
			
			EditorGUI.EndProperty ();
		}
	}
}