/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace NUIExport.hudBattlePVP1
{
	public partial class UI_main : GComponent
	{
		public UI_leftJoystick m_leftJoystick;
		public UI_rightJoystick m_rightJoystick;

		public const string URL = "ui://d39c1qv4el7u0";

		public static UI_main CreateInstance()
		{
			return (UI_main)UIPackage.CreateObject("hudBattlePVP1","main");
		}

		public UI_main()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_leftJoystick = (UI_leftJoystick)this.GetChildAt(0);
			m_rightJoystick = (UI_rightJoystick)this.GetChildAt(1);
		}
	}
}