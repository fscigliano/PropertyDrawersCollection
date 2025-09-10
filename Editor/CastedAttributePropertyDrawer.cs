using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

namespace com.fscigliano.PropertyDrawersCollection.Editor
{
    /// <summary>
    /// Creation Date:   23/02/2020 20:43:19
    /// Product Name:    Property Drawers Collection
    /// Developers:      Franco Scigliano
    /// Description:     
    /// </summary>
	public class CastedAttributePropertyDrawer<T> : PropertyDrawer where T:PropertyAttributeBase
    {
        #region Properties, Consts and Statics
		protected virtual List<SerializedPropertyType> validTypes { get { return new List<SerializedPropertyType> (new SerializedPropertyType[] { SerializedPropertyType.ObjectReference }); } }
        protected T cAttribute => (T)attribute;
        #endregion
        #region Methods
		public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
		{
			label = string.IsNullOrEmpty (cAttribute.label) ? label : new GUIContent (cAttribute.label, cAttribute.tooltip);
			if (validTypes != null && !validTypes.Contains(property.propertyType))
			{
				string validTypesString = string.Empty;
				foreach (SerializedPropertyType spt in validTypes)
				{
					if (string.IsNullOrEmpty(validTypesString))
						validTypesString+=spt.ToString();
					else
						validTypesString+=","+spt;
				}
				EditorGUI.HelpBox(position, typeof(T)+" can only be used with: "+validTypesString, MessageType.Error); 
				return;
			}
			DoOnGUI(position, property, label);
		}

		protected virtual void DoOnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
		}
		#endregion
	}
}