using System;

namespace com.fscigliano.PropertyDrawersCollection
{
    /// <summary>
    /// Creation Date:   01/02/2020 22:51:22
    /// Product Name:    Property Drawers Collection
    /// Developers:      Franco Scigliano
    /// Description:     Save and shows several values in a bitmask type enum.
    /// </summary>
	[AttributeUsage(AttributeTargets.Field, Inherited = true)]
	public class EnumFlagsAttribute : PropertyAttributeBase
	{
		public EnumFlagsAttribute() { }
	}
}