using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace com.fscigliano.PropertyDrawersCollection.Editor
{
    /// <summary>
    /// Creation Date:   01/02/2020 22:44:10
    /// Product Name:    Property Drawers Collection
    /// Developers:      Franco Scigliano
    /// Description:     Draws a range between 2 ints, 2 floats or the values of 2 properties.
    ///                  Valid for values of type int and float.
    /// </summary>
	[CustomPropertyDrawer(typeof(CustomRangeAttribute))]
	public class CustomRangeAttributeDrawer : CastedAttributePropertyDrawer<CustomRangeAttribute>
	{
		protected override List<SerializedPropertyType> validTypes { get { return new List<SerializedPropertyType> (new[] { SerializedPropertyType.Integer, SerializedPropertyType.Float }); } }

        protected override void DoOnGUI(Rect position, SerializedProperty property, GUIContent label)
	    {
			float minLimit = cAttribute.min;
			float maxLimit = cAttribute.max;

			if (cAttribute.reflected)
			{
				minLimit = MinValueForProperty(property, cAttribute.minPropertyName);
				maxLimit = MaxValueForProperty(property, cAttribute.maxPropertyName);
			}
			EditorGUI.BeginProperty (position, label, property);
			if (property.propertyType == SerializedPropertyType.Integer)
			{
				float floatValue = property.intValue;
				float newfloatValue = EditorGUI.Slider(position, label, floatValue, minLimit, maxLimit);
				if (floatValue != newfloatValue)
					property.intValue = Mathf.RoundToInt(newfloatValue);
			}
			else if (property.propertyType == SerializedPropertyType.Float)
			{
				property.floatValue = EditorGUI.Slider(position, label, property.floatValue, minLimit, maxLimit);
			}
			EditorGUI.EndProperty();

	    }
		public float MinValueForProperty( SerializedProperty prop, string minPropertyName)
		{		
			var minProp = prop.serializedObject.FindProperty(minPropertyName);
			if(minProp == null)
			{
				Debug.LogWarning("Invalid min property name in ReflectedRangeAttribute");
				return 0.0f;
			}
			return ValueForProperty(minProp); 
		} 

		public float MaxValueForProperty(SerializedProperty prop, string maxPropertyName)
		{
			var maxProp = prop.serializedObject.FindProperty(maxPropertyName);
			if(maxProp == null)
			{
				Debug.LogWarning("Invalid max property name in ReflectedRangeAttribute");
				return 0.0f;
			}
			return ValueForProperty(maxProp); 
		}

		public float ValueForProperty(SerializedProperty prop)
		{
			switch(prop.propertyType)
			{
			case SerializedPropertyType.Integer:
				return prop.intValue;
			case SerializedPropertyType.Float:
				return prop.floatValue;
			default:
				return 0.0f;
			}
		}
	}
}