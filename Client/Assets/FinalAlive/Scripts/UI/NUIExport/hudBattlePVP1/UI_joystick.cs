/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace NUIExport.hudBattlePVP1
{
	public partial class UI_joystick : GComponent
	{
		public GImage m_joystick_center;
		public UI_joystickCircle m_joystick;

		public const string URL = "ui://d39c1qv4h0hy3";

		public static UI_joystick CreateInstance()
		{
			return (UI_joystick)UIPackage.CreateObject("hudBattlePVP1","joystick");
		}

		public UI_joystick()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_joystick_center = (GImage)this.GetChildAt(0);
			m_joystick = (UI_joystickCircle)this.GetChildAt(1);
		}
	}
}