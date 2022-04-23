using System.Linq;
using FairyGUI;
using UnityEngine;

namespace ET
{
	[ChildType(typeof(WindowCoreData))]
	public class UIWindow: Entity,IAwake<GObject>
	{
		public bool IsPreLoad
		{
			get
			{
				return this.mGObject != null;
			}
		}

		public GComponent uiPanel
		{
			get
			{
				if (null != this.mPanel)
				{
					return this.mPanel;
				}
				return null;
			}
		}

		public WindowId WindowId
		{
			get
			{
				if (this.m_windowId == WindowId.Invaild)
				{
					Debug.LogError("window id is " + WindowId.Invaild);
				}
				return m_windowId;
			}
			set { m_windowId = value; }
		}
		
		public WindowId m_windowId = WindowId.Invaild;

		public GObject mGObject = null;
		public GComponent mPanel = null;

		public WindowCoreData WindowData = null;

		public override void Dispose()
		{
			if (this.IsDisposed)
			{
				return;
			}
			base.Dispose();
		}
	}
}