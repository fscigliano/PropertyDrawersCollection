using System;

namespace com.fscigliano.PropertyDrawersCollection
{
    /// <summary>
    /// Creation Date:   24/02/2020 23:03:51
    /// Product Name:    Property Drawers Collection
    /// Developers:      Franco Scigliano
    /// Description:     Draws a TextArea with a string that can be a fixed text, the content of a property, or
    ///                  the content of the string that the attribute is decorating.
    ///                  Valid for values of type string.
    /// </summary>
    [AttributeUsage( AttributeTargets.Field, Inherited = true)]
    public class DescriptionAttribute : PropertyAttributeBase
    {
        public bool hasParameter = false;
        public string classDescription;
        
        public DescriptionAttribute()
        {
            hasParameter = false;
        }
        public DescriptionAttribute(string classDescription)
        {
            hasParameter = true;
            this.classDescription = classDescription;
        }
    }
}