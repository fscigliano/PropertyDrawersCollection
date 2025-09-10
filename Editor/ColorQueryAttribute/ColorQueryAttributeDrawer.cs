using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace com.fscigliano.PropertyDrawersCollection.Editor
{
    /// <summary>
    /// Creation Date:   01/02/2020 22:45:20
    /// Product Name:    Property Drawers Collection
    /// Developers:      Franco Scigliano
    /// Description:     Paints the background of a property with a color if the object is null, or another color otherwise.
    ///                  Valid for values of type string and object.
    /// </summary>
	[CustomPropertyDrawer(typeof(ColorQueryAttribute), true)]
	public class ColorQueryAttributeDrawer : CastedAttributePropertyDrawer<ColorQueryAttribute>
	{
		protected override List<SerializedPropertyType> validTypes { get { return new List<SerializedPropertyType> (new SerializedPropertyType[] { SerializedPropertyType.String, SerializedPropertyType.ObjectReference }); } }
		
        protected override void DoOnGUI (Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);
			Color oldcolor = GUI.backgroundColor;

			Color nullColor =  GetColor(cAttribute.NullColor);
			Color notNullColor = GetColor(cAttribute.NotNullColor);

			if (property.propertyType.ToString() == "String")
			{
				if (string.IsNullOrEmpty(property.stringValue))
					GUI.backgroundColor = nullColor;
				else
					GUI.backgroundColor = notNullColor;
				
				property.stringValue = EditorGUI.TextField(position, label, property.stringValue);
			}
			else if (property.propertyType.ToString() == "ObjectReference")
			{
				if (property.objectReferenceValue == null)
					GUI.backgroundColor = nullColor;
				else
					GUI.backgroundColor = notNullColor;
				
				EditorGUI.ObjectField(position, property, label);
			}

			GUI.backgroundColor = oldcolor;
			EditorGUI.EndProperty();
		}
		protected Color GetColor(QueryColor c)
		{
			switch (c)
			{
				case QueryColor.BLACK:
					return Color.black;
				case QueryColor.BLUE:
					return Color.blue;
				case QueryColor.CLEAR:
					return Color.clear;
				case QueryColor.CYAN:
					return Color.cyan;
				case QueryColor.GRAY:
					return Color.gray;
				case QueryColor.GREEN:
					return Color.green;
				case QueryColor.MAGENTA:
					return Color.magenta;
				case QueryColor.RED:
					return Color.red;
				case QueryColor.WHITE:
					return Color.white;
				case QueryColor.YELLOW:
					return Color.yellow;
			}
			return default(Color);
		}
	}
}