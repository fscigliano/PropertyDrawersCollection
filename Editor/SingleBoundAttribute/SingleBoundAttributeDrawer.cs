using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace com.fscigliano.PropertyDrawersCollection.Editor
{
    /// <summary>
    /// Creation Date:   01/02/2020 22:51:22
    /// Product Name:    Property Drawers Collection
    /// Developers:      Franco Scigliano
    /// Description:	 original version available in https://github.com/anchan828/property-drawer-collection
    /// </summary>
	public abstract class SingleBoundAttributeDrawer<T> : CastedAttributePropertyDrawer<T> where T:SingleBoundAttribute
	{
		protected override List<SerializedPropertyType> validTypes { get { return new List<SerializedPropertyType> (new SerializedPropertyType[] { SerializedPropertyType.Float, SerializedPropertyType.Integer }); } }

        protected override void DoOnGUI (Rect position,
			SerializedProperty property,
			GUIContent label) 
		{

			EditorGUI.BeginProperty (position, label, property);
			if (property.propertyType == SerializedPropertyType.Integer)
			{
				property.intValue = IntGet(EditorGUI.IntField(position, label, property.intValue),
					cAttribute.IntBound);
			}
			else if (property.propertyType == SerializedPropertyType.Float)
			{
				property.floatValue = FloatGet(EditorGUI.FloatField(position, label, property.floatValue),
					cAttribute.FloatBound);
			}
			EditorGUI.EndProperty();
		}
		protected abstract float FloatGet(float a, float b);
		protected abstract int IntGet(int a, int b);

	}
}
