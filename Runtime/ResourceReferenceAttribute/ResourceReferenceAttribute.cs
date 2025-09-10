using System;

namespace com.fscigliano.PropertyDrawersCollection
{
    /// <summary>
    /// Creation Date:   01/02/2020 22:51:22
    /// Product Name:    Property Drawers Collection
    /// Developers:      Franco Scigliano
    /// Description:     
    /// </summary>
	[AttributeUsage( AttributeTargets.Field, Inherited = true)]
	public class ResourceReferenceAttribute : PropertyAttributeBase
	{
		public string filter = null;
		public Type type;
		
		public ResourceReferenceAttribute()
		{

		}
		public ResourceReferenceAttribute(string filter)
		{
			this.filter = filter;
		}
		public ResourceReferenceAttribute(Type type)
		{
			this.type = type;
		}
		public ResourceReferenceAttribute(string filter, Type type)
		{
			this.filter = filter;
			this.type = type;
		}
	}
}