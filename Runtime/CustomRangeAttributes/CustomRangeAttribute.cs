using System;

namespace com.fscigliano.PropertyDrawersCollection
{
    /// <summary>
    /// Creation Date:   01/02/2020 22:51:22
    /// Product Name:    Property Drawers Collection
    /// Developers:      Franco Scigliano
    /// Description:     Draws a range between 2 ints, 2 floats or the values of 2 properties.
    ///                  Valid for values of type int and float.
    /// </summary>
	[AttributeUsage(AttributeTargets.Field, Inherited = true)]
	public class CustomRangeAttribute : PropertyAttributeBase
	{
	    public float min;
	    public float max;
		public bool reflected = false;
		public bool IsInt = false;
		public string minPropertyName;
		public string maxPropertyName;

	    public CustomRangeAttribute(int min, int max)
		{
			reflected = false;
	        this.min = min;
	        this.max = max;
			this.IsInt = true;
	    }
		public CustomRangeAttribute(string minPropertyName, string maxPropertyName)
		{
			reflected = true;
			this.minPropertyName = minPropertyName; 
			this.maxPropertyName = maxPropertyName;
			this.IsInt = false;
		}
		public CustomRangeAttribute(float min, float max)
		{
			reflected = false;
			this.min = min;
			this.max = max;
			this.IsInt = false;
		}
	}
}