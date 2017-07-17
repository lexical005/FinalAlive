using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;

namespace NUIManagerInternal
{
	/// <summary>
	/// FairyGUI包的状态
	/// </summary>
	public class FairyPackage
	{
		/// <summary>
		/// 包的加载状态
		/// </summary>
		private enum ELoadState
		{
			/// <summary>
			/// 未加载或已卸载
			/// </summary>
			Unload,

			/// <summary>
			/// 已加载
			/// </summary>
			Loaded,

			/// <summary>
			/// 已标记等待卸载
			/// </summary>
			WaitUnload,
		}

		/// <summary>
		/// 包名
		/// </summary>
		private string namePackage;

		/// <summary>
		/// 生命周期
		/// </summary>
		private readonly ELifeScope lifeScope;

		/// <summary>
		/// 当前状态
		/// </summary>
		private ELoadState state = ELoadState.Unload;


		/// <summary>
		/// 构造包
		/// </summary>
		/// <param name="namePackage"></param>
		/// <param name="lifeScope"></param>
		public FairyPackage(string namePackage, ELifeScope lifeScope)
		{
			this.namePackage = namePackage;
			this.lifeScope = lifeScope;

			Load();
		}

		/// <summary>
		/// 立即加载包
		/// </summary>
		public void Load()
		{
			if (this.state == ELoadState.Unload)
			{
				UIPackage.AddPackage(NUIExport.Binder.mapPackage[namePackage]);
			}
			this.state = ELoadState.Loaded;
		}

		/// <summary>
		/// 卸载包
		/// </summary>
		/// <param name="immediately">是否立即移除</param>
		public void Unload(bool immediately)
		{
			if (immediately)
			{
				// 立即移除
				if (this.state != ELoadState.Unload)
				{
					UIPackage.RemovePackage(namePackage);
					this.state = ELoadState.Unload;
				}
			}
			else
			{
				// 标记等待移除
				if (this.state == ELoadState.Loaded)
				{
					this.state = ELoadState.Unload;
				}
			}
		}

	#if UNITY_EDITOR
		/// <summary>
		/// 校验生命周期是否持续一致
		/// </summary>
		/// <param name="lifeScope"></param>
		/// <returns></returns>
		public void CheckScope(ELifeScope lifeScope)
		{
			if (this.lifeScope != lifeScope)
			{
				Debug.LogErrorFormat("not allow package[{0}] lifeScope change: {1}->{2}", namePackage, this.lifeScope, lifeScope);
			}
		}
	#endif
	}
}
