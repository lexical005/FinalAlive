
/** This is an automatically generated class by BindExport. Do not modify it. **/

using FairyGUI;
using System.Collections.Generic;

namespace NUIExport
{
	public class Binder
	{
		/// <summary>
		/// 包名到资源定义的映射
		/// </summary>
		public static readonly Dictionary<string, string> mapPackage = new Dictionary<string, string>()
		{
            { "basic", "UI/basic" },
            { "funcBag", "UI/funcBag" },
            { "hudBattlePVP1", "UI/hudBattlePVP1" },
            { "hudHome", "UI/hudHome" },
            { "hudLogin", "UI/hudLogin" },
            { "transit", "UI/transit" },
		};

		/// <summary>
		/// 自定义组件类型绑定
		/// </summary>
		public static void BindAll()
		{
            NUIExport.basic.basicBinder.BindAll();
            NUIExport.funcBag.funcBagBinder.BindAll();
            NUIExport.hudBattlePVP1.hudBattlePVP1Binder.BindAll();
            NUIExport.hudHome.hudHomeBinder.BindAll();
            NUIExport.hudLogin.hudLoginBinder.BindAll();
            NUIExport.transit.transitBinder.BindAll();
		}
	}
}
