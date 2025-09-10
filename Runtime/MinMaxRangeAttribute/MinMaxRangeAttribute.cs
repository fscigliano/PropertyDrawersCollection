using System;

namespace com.fscigliano.PropertyDrawersCollection
{
    /// <summary>
    /// Creation Date:   01/02/2020 22:51:22
    /// Product Name:    Property Drawers Collection
    /// Developers:      Franco Scigliano
    /// Description:     original version available in https://github.com/uranuno/MyPropertyDrawers
    /// </summary>
	[AttributeUsage(AttributeTargets.Field, Inherited = true)]
	public class MinMaxRangeAttribute : PropertyAttributeBase {		

		public float minLimit;
		public float maxLimit;
		public string minPropertyName;
		public string maxPropertyName;
		public bool IsInt = false;
		public bool reflected = false;

		public MinMaxRangeAttribute (string minPropertyName, string maxPropertyName) {
			this.reflected =true;
			this.minPropertyName = minPropertyName;
			this.maxPropertyName = maxPropertyName;
			this.IsInt = true;
		}
		public MinMaxRangeAttribute (int minLimit, int maxLimit) {

			this.reflected = false;
			this.minLimit = minLimit;
			this.maxLimit = maxLimit;
			this.IsInt = true;
		}
		public MinMaxRangeAttribute (float minLimit, float maxLimit) {

			this.reflected = false;
			this.minLimit = minLimit;
			this.maxLimit = maxLimit;
			this.IsInt = false;
		}
	}
}