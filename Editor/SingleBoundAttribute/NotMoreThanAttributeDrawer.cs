using UnityEditor;
using UnityEngine;

namespace com.fscigliano.PropertyDrawersCollection.Editor
{
    /// <summary>
    /// Creation Date:   01/02/2020 22:51:22
    /// Product Name:    Property Drawers Collection
    /// Developers:      Franco Scigliano
    /// Description:	 original version available in https://github.com/anchan828/property-drawer-collection
    /// </summary>
	[CustomPropertyDrawer(typeof(NotMoreThanAttribute))]
	public class NotMoreThanAttributeDrawer : SingleBoundAttributeDrawer<NotMoreThanAttribute>
	{
		protected override int IntGet (int a, int b)
		{
			return Mathf.Min(a,b);
		}
		protected override float FloatGet (float a, float b)
		{
			return Mathf.Min(a,b);
		}
	}
}