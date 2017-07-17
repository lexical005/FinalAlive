/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace NUIExport.funcBag
{
	public class UI_main : GComponent
	{
		public GComponent m_frame;

		public const string URL = "ui://fdzfwyaac6070";

		public static UI_main CreateInstance()
		{
			return (UI_main)UIPackage.CreateObject("funcBag","main");
		}

		public UI_main()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_frame = (GComponent)this.GetChildAt(0);
		}
	}
}