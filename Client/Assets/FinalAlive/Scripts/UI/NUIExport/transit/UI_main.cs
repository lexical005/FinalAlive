/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace NUIExport.transit
{
	public partial class UI_main : GComponent
	{
		public GProgressBar m_loading;

		public const string URL = "ui://45zlubrmrzie0";

		public static UI_main CreateInstance()
		{
			return (UI_main)UIPackage.CreateObject("transit","main");
		}

		public UI_main()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_loading = (GProgressBar)this.GetChildAt(1);
		}
	}
}