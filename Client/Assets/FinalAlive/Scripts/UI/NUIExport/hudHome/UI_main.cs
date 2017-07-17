/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace NUIExport.hudHome
{
	public partial class UI_main : GComponent
	{
		public GButton m_role;
		public GButton m_bag;
		public GButton m_weapon;
		public GButton m_pet;
		public GButton m_battle;

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

			m_role = (GButton)this.GetChildAt(0);
			m_bag = (GButton)this.GetChildAt(1);
			m_weapon = (GButton)this.GetChildAt(2);
			m_pet = (GButton)this.GetChildAt(3);
			m_battle = (GButton)this.GetChildAt(4);
		}
	}
}