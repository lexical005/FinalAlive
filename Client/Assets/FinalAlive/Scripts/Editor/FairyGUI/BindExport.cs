using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace NFairyGUIEdior
{
	public class BindExport
	{
        /// <summary>
        /// FairyGUI的代码导出目录
        /// </summary>
        private const string fairyGUICodeExportPath = "Assets/FinalAlive/Scripts/UI/NUIExport";


		/// <summary>
		/// FairyGUI的资源导出目录
		/// </summary>
		private static string fairyGUIResExportPath = "UI/";


		/// <summary>
		/// FairyGUI导出的代码的命名空间
		/// </summary>
		private const string fairyGUIExportNamespace = "NUIExport";

		private const string bindFileFormat = @"
/** This is an automatically generated class by BindExport. Do not modify it. **/

using FairyGUI;
using System.Collections.Generic;

namespace NUIExport
{{
	public class Binder
	{{
		/// <summary>
		/// 包名到资源定义的映射
		/// </summary>
		public static readonly Dictionary<string, string> mapPackage = new Dictionary<string, string>()
		{{
{0}
		}};

		/// <summary>
		/// 自定义组件类型绑定
		/// </summary>
		public static void BindAll()
		{{
{1}
		}}
	}}
}}
";

		[MenuItem("FairyGUI/Bind Export")]
		public static void Builde()
		{
			// 包映射
			StringBuilder mapPackageBuilder = new StringBuilder();

			// 绑定
			StringBuilder bindAllBuilder = new StringBuilder();

			DirectoryInfo dirInfo = new DirectoryInfo(fairyGUICodeExportPath);
			foreach (DirectoryInfo subDirInfo in dirInfo.GetDirectories())
			{
				foreach (FileInfo f in subDirInfo.GetFiles("*Binder.cs", SearchOption.TopDirectoryOnly)) //查找文件
				{
					string namePackage = subDirInfo.Name;
					string nameRootComponent = f.Name.Substring(0, f.Name.Length - 3);

					if (mapPackageBuilder.Length > 0)
					{
						mapPackageBuilder.Append("\n");
					}
					mapPackageBuilder.Append(string.Format("            {{ \"{0}\", \"{1}\" }},", namePackage, fairyGUIResExportPath + namePackage));


					if (bindAllBuilder.Length > 0)
					{
						bindAllBuilder.Append("\n");
					}
					bindAllBuilder.Append("            " + fairyGUIExportNamespace + "." + namePackage + "." + nameRootComponent + ".BindAll();");
				}
			}

			File.WriteAllText(Path.Combine(fairyGUICodeExportPath, "Binder.cs"), string.Format(bindFileFormat, mapPackageBuilder.ToString(), bindAllBuilder.ToString()), Encoding.UTF8);

			AssetDatabase.Refresh();
		}
	}
}
