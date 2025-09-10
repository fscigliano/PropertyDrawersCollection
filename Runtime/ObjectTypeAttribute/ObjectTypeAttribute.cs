namespace com.fscigliano.PropertyDrawersCollection
{
    /// <summary>
    /// Creation Date:   10/03/2020 10:53:11
    /// Product Name:    Property Drawers Collection
    /// Developers:      Franco Scigliano
    /// Description:     This attribute allows an Object to be referenced in the editor according to it's interface.
    /// </summary>
    public class ObjectTypeAttribute : PropertyAttributeBase
    {
        public System.Type FilterType;

        public ObjectTypeAttribute(System.Type filterType)
        {
            FilterType = filterType;
        }
    }
}