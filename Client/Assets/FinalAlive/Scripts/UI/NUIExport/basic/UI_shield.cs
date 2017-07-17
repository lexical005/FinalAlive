/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace NUIExport.basic
{
	public partial class UI_shield : GComponent
	{
		public GGraph m_shield;
		public Transition m_fadein;
		public Transition m_fadeout;

		public const string URL = "ui://iy83z0owrzie46";

		public static UI_shield CreateInstance()
		{
			return (UI_shield)UIPackage.CreateObject("basic","shield");
		}

		public UI_shield()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_shield = (GGraph)this.GetChildAt(0);
			m_fadein = this.GetTransitionAt(0);
			m_fadeout = this.GetTransitionAt(1);
		}
	}
}