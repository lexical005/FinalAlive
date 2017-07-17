using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace NFairyGUIEdior
{
	public class BuildAssetBundles
    {
        /// <summary>
        /// FairyGUI的资源导出目录
        /// </summary>
        private const string fairyGUIAssetExportPath = "Assets/Resources/UI";


        [MenuItem("FairyGUI/Build AssetBundles")]
		public static void Builde()
		{
            //for (int i = 0; i < 10; i++)
            //{
            // AssetImporter.GetAtPath("Assets/FairyGUI/Examples/Resources/Icons/i" + i + ".png").assetBundleName = "fairygui-examples/i" + i + ".ab";
            //}

            //AssetImporter.GetAtPath("Assets/FairyGUI/Examples/Resources/UI/BundleUsage.bytes").assetBundleName = "fairygui-examples/bundleusage.ab";
            //AssetImporter.GetAtPath("Assets/FairyGUI/Examples/Resources/UI/BundleUsage@sprites.bytes").assetBundleName = "fairygui-examples/bundleusage.ab";
            //AssetImporter.GetAtPath("Assets/FairyGUI/Examples/Resources/UI/BundleUsage@atlas0.png").assetBundleName = "fairygui-examples/bundleusage.ab";

            //BuildPipeline.BuildAssetBundles(Application.streamingAssetsPath, BuildAssetBundleOptions.None, BuildTarget.Android);

            List<string> allFiles = new List<string>(128);
            DirectoryInfo dirInfo = new DirectoryInfo(fairyGUIAssetExportPath);
            foreach (FileInfo f in dirInfo.GetFiles("*", SearchOption.TopDirectoryOnly)) //查找文件
            {
                if (f.Name.EndsWith(".meta"))
                {
                    continue;
                }
                allFiles.Add(f.Name);
            }

            allFiles.Sort();

            for (int i = 0; i < allFiles.Count;)
            {
                string namePackage = allFiles[i].Split('.')[0];
                string packageDefineAssetBundleName = "fairygui/" + namePackage + "-def.ab";
                string packageResourceAssetBundleName = "fairygui/" + namePackage + "-res.ab";

                string packageDefineFullPath = fairyGUIAssetExportPath + "/" + allFiles[i];
                AssetImporter.GetAtPath(packageDefineFullPath).assetBundleName = packageDefineAssetBundleName;

                string packageResourcePrefix = namePackage + "@";
                int j = i + 1;
                for (; j < allFiles.Count; ++j)
                {
                    if (!allFiles[j].StartsWith(packageResourcePrefix))
                    {
                        break;
                    }

                    string packageResourceFullPath = fairyGUIAssetExportPath + "/" + allFiles[j];
                    AssetImporter.GetAtPath(packageResourceFullPath).assetBundleName = packageResourceAssetBundleName;
                }

                i = j;
            }

            AssetDatabase.Refresh();

            //AssetImporter.GetAtPath("Assets/Resources/UI/BundleUsage@atlas0.png").assetBundleName = "fairygui-examples/bundleusage.ab";
        }
	}
}
