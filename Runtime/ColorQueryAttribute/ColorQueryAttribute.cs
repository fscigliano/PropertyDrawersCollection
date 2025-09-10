using System;

namespace com.fscigliano.PropertyDrawersCollection
{
    /// <summary>
    /// Creation Date:   01/02/2020 22:51:22
    /// Product Name:    Property Drawers Collection
    /// Developers:      Franco Scigliano
    /// Description:     Paints the background of a property with a color if the object is null, or another color otherwise.
    ///                  Valid for values of type string and object.
    /// </summary>
	public enum QueryColor
	{
		NOT_SET = -1, BLACK=0, BLUE = 1, CLEAR = 2, CYAN = 3, GRAY=4, GREEN=5, MAGENTA =6, RED =7, WHITE =8, YELLOW =9
	}
	[AttributeUsage( AttributeTargets.Field, Inherited = true)]
	public class ColorQueryAttribute : PropertyAttributeBase 
	{
		public QueryColor NullColor = QueryColor.NOT_SET;
		public QueryColor NotNullColor = QueryColor.NOT_SET;

		public ColorQueryAttribute()
		{
			this.NullColor = QueryColor.RED;
			this.NotNullColor = QueryColor.WHITE;
		}
		public ColorQueryAttribute(QueryColor NullColor)
		{
			this.NullColor = NullColor;
			this.NotNullColor = QueryColor.WHITE;
		}
		public ColorQueryAttribute(QueryColor NullColor, QueryColor NotNullColor)
		{
			this.NullColor = NullColor;
			this.NotNullColor = NotNullColor;
		}
	}
}