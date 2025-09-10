using System;

namespace com.fscigliano.PropertyDrawersCollection
{
	/// <summary>
    /// Creation Date:   01/02/2020 22:51:22
    /// Product Name:    Property Drawers Collection
    /// Developers:      Franco Scigliano
    /// Description:     Shows a info, warning or error box with text content.
    ///                  original version available in http://baba-s.hatenablog.com/entry/2014/08/20/112256
    /// </summary>
	[AttributeUsage( AttributeTargets.All, Inherited = true, AllowMultiple = true )]
	public sealed class HelpBoxAttribute : PropertyAttributeBase
	{
        public string Message;
		
		public HelpBoxType Type;

		public HelpBoxAttribute( string message)
		{
			Message     = message;
			Type        = HelpBoxType.None;
			this.order  = 0;
		}
		public HelpBoxAttribute( string message, HelpBoxType type)
		{
			Message     = message;
			Type        = type;
			this.order  = 0;
		}
		public HelpBoxAttribute( string message, HelpBoxType type, int order)
		{
			Message     = message;
			Type        = type;
			this.order  = order;
		}
	}
}