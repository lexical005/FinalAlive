/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace NUIExport.hudLogin
{
	public class UI_main : GComponent
	{
		public FairyGUI.Controller m_page;
		public GTextField m_tip;
		public GComponent m_login;
		public GTextInput m_account;
		public GTextInput m_password;
		public GComponent m_enter;

		public const string URL = "ui://dnyqgmofel7u0";

		public static UI_main CreateInstance()
		{
			return (UI_main)UIPackage.CreateObject("hudLogin","main");
		}

		public UI_main()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_page = this.GetControllerAt(0);
			m_tip = (GTextField)this.GetChildAt(0);
			m_login = (GComponent)this.GetChildAt(1);
			m_account = (GTextInput)this.GetChildAt(6);
			m_password = (GTextInput)this.GetChildAt(7);
			m_enter = (GComponent)this.GetChildAt(9);
		}
	}
}