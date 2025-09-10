using UnityEngine;
using UnityEditor;

namespace com.fscigliano.PropertyDrawersCollection.Editor
{
    /// <summary>
    /// Creation Date:   01/02/2020 22:43:40
    /// Product Name:    Property Drawers Collection
    /// Developers:      Franco Scigliano
    /// Description:     Shows a info, warning or error box with text content.
    ///                  original version available in http://baba-s.hatenablog.com/entry/2014/08/20/112256
    /// </summary>
	[CustomPropertyDrawer( typeof( HelpBoxAttribute ) )]
	public sealed class HelpBoxDrawer : CastedDecoratorDrawer<HelpBoxAttribute>
	{
		public override void OnGUI( Rect position )
		{
			var helpBoxPosition = EditorGUI.IndentedRect( position );
			helpBoxPosition.height = GetHelpBoxHeight();
			
			EditorGUI.HelpBox( helpBoxPosition, cAttribute.Message, GetMessageType( cAttribute.Type ) );
		}
		
		public override float GetHeight()
		{
			return GetHelpBoxHeight();
		}
		
		public MessageType GetMessageType( HelpBoxType type )
		{
			switch ( type )
			{
				case HelpBoxType.Error:     return MessageType.Error;
				case HelpBoxType.Info:      return MessageType.Info;
				case HelpBoxType.None:      return MessageType.None;
				case HelpBoxType.Warning:   return MessageType.Warning;
			}
			return 0;
		}
		
		public float GetHelpBoxHeight()
		{
			var style   = new GUIStyle( "HelpBox" );
			var content = new GUIContent( cAttribute.Message );
			return Mathf.Max( style.CalcHeight( content, Screen.width - ( cAttribute.Type != HelpBoxType.None ? 53 : 21) ), 40);
		}
	}
}