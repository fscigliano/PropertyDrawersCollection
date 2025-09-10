using System;

namespace com.fscigliano.PropertyDrawersCollection
{
    /// <summary>
    /// Creation Date:   29/02/2020 16:44:14
    /// Product Name:    Property Drawers Collection
    /// Developers:      Franco Scigliano
    /// Description:     Shows a button that set the icon for the script file as the one of the parameter value.
    ///                  Useful to fix inherited classes icons.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = true)]
    public class FixIconAttribute : PropertyAttributeBase
    {
        #region Editor Variables
        public string IconFileName;
        public bool hideProperty;
        #endregion
                        
        #region Methods
        public FixIconAttribute(string IconFileName)
        {
            this.IconFileName = IconFileName;
            hideProperty = false;
        }
        public FixIconAttribute(string IconFileName, bool hideProperty)
        {
            this.IconFileName = IconFileName;
            this.hideProperty = hideProperty;
        }
        #endregion
    }
}