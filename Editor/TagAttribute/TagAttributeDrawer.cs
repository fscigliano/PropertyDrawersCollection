using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace com.fscigliano.PropertyDrawersCollection.Editor
{
    /// <summary>
    /// Creation Date:   01/02/2020 22:51:22
    /// Product Name:    Property Drawers Collection
    /// Developers:      Franco Scigliano
    /// Description:	 original version available in https://github.com/uranuno/MyPropertyDrawers
    /// </summary>
	[CustomPropertyDrawer(typeof(TagAttribute))]
	public class TagAttributeDrawer : CastedAttributePropertyDrawer<TagAttribute> {

		protected override List<SerializedPropertyType> validTypes { get { return new List<SerializedPropertyType> (new SerializedPropertyType[] { SerializedPropertyType.String }); } }

        protected override void DoOnGUI(Rect position, SerializedProperty property, GUIContent label) 
		{
			EditorGUI.BeginProperty (position, label, property);
			
			property.stringValue = EditorGUI.TagField(position, label, property.stringValue);
			
			EditorGUI.EndProperty ();
		}
	}
}