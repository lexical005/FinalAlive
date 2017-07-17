/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace NUIExport.basic
{
	public partial class UI_windowFrame1 : GLabel
	{
		public FairyGUI.Controller m_button;
		public GButton m_btnClose;

		public const string URL = "ui://iy83z0owiuhe3z";

		public static UI_windowFrame1 CreateInstance()
		{
			return (UI_windowFrame1)UIPackage.CreateObject("basic","windowFrame1");
		}

		public UI_windowFrame1()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_button = this.GetControllerAt(0);
			m_btnClose = (GButton)this.GetChildAt(1);
		}
	}
}