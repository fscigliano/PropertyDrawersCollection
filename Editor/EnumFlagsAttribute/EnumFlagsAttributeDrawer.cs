using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace com.fscigliano.PropertyDrawersCollection.Editor
{
    /// <summary>
    /// Creation Date:   01/02/2020 22:46:05
    /// Product Name:    Property Drawers Collection
    /// Developers:      Franco Scigliano
    /// Description:	 Save and shows several values in a bitmask type enum.
    /// </summary>
	[CustomPropertyDrawer(typeof(EnumFlagsAttribute))]
	public class EnumFlagsAttributeDrawer : CastedAttributePropertyDrawer<EnumFlagsAttribute>
	{
		protected override List<SerializedPropertyType> validTypes { get { return new List<SerializedPropertyType> (new SerializedPropertyType[] { SerializedPropertyType.Enum }); } }

        protected override void DoOnGUI(Rect _position, SerializedProperty _property, GUIContent _label)
		{
			_property.intValue = EditorGUI.MaskField( _position, _label, _property.intValue, _property.enumNames );
		}
	}
}
	