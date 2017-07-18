/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace NUIExport.hudBattlePVP1
{
	public partial class UI_joystickCircle : GButton
	{
		public GImage m_circle;

		public const string URL = "ui://d39c1qv4h0hy4";

		public static UI_joystickCircle CreateInstance()
		{
			return (UI_joystickCircle)UIPackage.CreateObject("hudBattlePVP1","joystickCircle");
		}

		public UI_joystickCircle()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_circle = (GImage)this.GetChildAt(0);
		}
	}
}