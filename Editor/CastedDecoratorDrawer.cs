using UnityEditor;

namespace com.fscigliano.PropertyDrawersCollection.Editor
{
    /// <summary>
    /// Creation Date:   23/02/2020 20:41:25
    /// Product Name:    Property Drawers Collection
    /// Developers:      Franco Scigliano
    /// Description:     
    /// </summary>
	public class CastedDecoratorDrawer<T> : DecoratorDrawer where T:PropertyAttributeBase
	{
        #region Properties, Consts and Statics
		protected T cAttribute => (T)attribute;
        #endregion
    }
}