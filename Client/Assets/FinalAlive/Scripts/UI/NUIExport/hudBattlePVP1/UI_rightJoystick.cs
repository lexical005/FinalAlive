/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace NUIExport.hudBattlePVP1
{
	public partial class UI_rightJoystick : GComponent
	{
		public GImage m_center;
		public UI_joystickCircle m_circle;

		public const string URL = "ui://d39c1qv4r0db8";

		public static UI_rightJoystick CreateInstance()
		{
			return (UI_rightJoystick)UIPackage.CreateObject("hudBattlePVP1","rightJoystick");
		}

		public UI_rightJoystick()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_center = (GImage)this.GetChildAt(0);
			m_circle = (UI_joystickCircle)this.GetChildAt(1);
		}
	}
}