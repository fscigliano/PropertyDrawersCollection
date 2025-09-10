using System;

namespace com.fscigliano.PropertyDrawersCollection
{
    /// <summary>
    /// Creation Date:   01/03/2020 16:51:22
    /// Product Name:    Property Drawers Collection
    /// Developers:      Franco Scigliano
    /// Description:     
    /// </summary>
	[AttributeUsage( AttributeTargets.Field, Inherited = true)]
	public class GetComponentAttribute : PropertyAttributeBase 
	{
		public GetComponentAttribute()
		{

		}
	}
}