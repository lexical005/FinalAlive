/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace NUIExport.hudHome
{
	public class UI_main : GComponent
	{
		public GComponent m_role;
		public GComponent m_bag;
		public GComponent m_weapon;
		public GComponent m_pet;

		public const string URL = "ui://deuco6u9iuhe0";

		public static UI_main CreateInstance()
		{
			return (UI_main)UIPackage.CreateObject("hudHome","main");
		}

		public UI_main()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_role = (GComponent)this.GetChildAt(0);
			m_bag = (GComponent)this.GetChildAt(1);
			m_weapon = (GComponent)this.GetChildAt(2);
			m_pet = (GComponent)this.GetChildAt(3);
		}
	}
}