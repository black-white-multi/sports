using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FairyGUI;
using FairyGUI.Utils;
using UnityEngine;

namespace ET
{

	[ObjectSystem]
	public class UIManageAwakeSystem : AwakeSystem<UIManage>
	{
		public override void Awake(UIManage self)
		{
			self.Awake();
		}
	}

	[ObjectSystem]
	public class UIManageLoadSystem : LoadSystem<UIManage>
	{
		public override void Load(UIManage self)
		{
			self.Load();
		}
	}

	[FriendClass(typeof(ShowWindowData))]
	[FriendClass(typeof(WindowCoreData))]
	[FriendClass(typeof(UIWindow))]
	[FriendClass(typeof(UIManage))]
	public static class UIManageSystem
	{
		public static void Awake(this UIManage self)
		{
			UIManage.Instance = self;

			//设置屏幕常亮
			Screen.sleepTimeout = SleepTimeout.NeverSleep;

			string path = "FGUI/";
			//加载UI包
			UIPackage.AddPackage($"{path}{UIManage.FGUIPackage}");

			FontManager.RegisterFont(FontManager.GetFont("Myriad-Bold"), "Myriad-Bold");
			UIConfig.defaultFont = "Myriad-Bold";

			GRoot.inst.SetContentScaleFactor(UIManage.DESIGN_SCREEN_WIDTH, UIManage.DESIGN_SCREEN_HEIGHT, UIContentScaler.ScreenMatchMode.MatchWidthOrHeight);
			self.Load();

			GameObject.DontDestroyOnLoad(StageCamera.main);
			//UniversalAdditionalCameraData uacd = StageCamera.main.GetUniversalAdditionalCameraData();
			//uacd.renderType = CameraRenderType.Overlay;
		}

		public static void Load(this UIManage self)
		{
			if (null != self.dicAllWindows)
			{
				self.dicAllWindows.Clear();
			}

			Type[] types = CodeLoader.Instance.GetTypes();
			foreach (Type type in types)
			{
				object[] attrs = type.GetCustomAttributes(typeof(UIViewAttribute), false);
				if (attrs.Length == 0)
				{
					continue;
				}

				UIViewAttribute attribute = attrs[0] as UIViewAttribute;
				if (self.dicAllWindows.ContainsKey(attribute.WindowId))
				{
					Log.Debug($"已经存在同类UIBaseView: {attribute.WindowId}");
					throw new Exception($"已经存在同类UIBaseView: {attribute.WindowId}");
				}
				self.dicAllWindows.Add(attribute.WindowId, type);
			}
		}

		public static UIWindow ShowUI(this UIManage self,WindowId windowId)
		{
            UIWindow ui = null;
            try
            {
                ui = self.ReadyToShowBaseWindow(windowId);
                if (ui != null)
                {
                    self.RealShowWindow(windowId, ui);
                }
            }
            catch (Exception e)
            {
                Log.Debug(e.ToString());
            }
            return ui;
        }

		private static UIWindow ReadyToShowBaseWindow(this UIManage self,WindowId windowId)
        {
			string fguiview = Enum.GetName(typeof(WindowId), windowId);
			Type view;
			self.dicAllWindows.TryGetValue(windowId, value: out view);
            if (view == null)
            {
                Log.Debug($"不存在UIBaseView: {fguiview}");
                throw new Exception($"不存在UIBaseView: {fguiview}");
            }

            GObject gObject = UIPackage.CreateObject(UIManage.FGUIPackage, fguiview);
            if (gObject == null)
            {
                Log.Debug($"{UIManage.FGUIPackage}不存在: {fguiview}");
                throw new Exception($"{UIManage.FGUIPackage}不存在: {fguiview}");
            }

            UIWindow ui = self.AddChild<UIWindow, GObject>(gObject);
			ui.WindowId = windowId;
			ui.AddComponent(view, false);

            return ui;
        }

        private static void RealShowWindow(this UIManage self, WindowId windowId, UIWindow ui)
        {
			string fguiview = Enum.GetName(typeof(WindowId), windowId);
			if (!self.dicShowWindows.Keys.Contains(windowId))
            {
				ui.Show();
				self.dicShowWindows.Add(windowId, ui);
				self.lastNavigationWindow = self.curNavigationWindow;
				self.curNavigationWindow = ui;
            }
            else
            {
                Log.Debug($"已存在UIBaseView: {fguiview}");
                throw new Exception($"已存在UIBaseView: {fguiview}");
            }
        }

        public static void CloseUI(this UIManage self, WindowId windowId)
        {
            UIWindow ui;
            if (!self.dicShowWindows.TryGetValue(windowId, out ui))
            {
                return;
            }
			self.dicShowWindows.Remove(windowId);
            ui.Close();
        }

        public static void CloseAllShowUI(this UIManage self)
        {
            foreach (WindowId type in self.dicShowWindows.Keys.ToArray())
            {
                UIWindow ui;
                if (!self.dicShowWindows.TryGetValue(type, out ui))
                {
                    continue;
                }
				self.dicShowWindows.Remove(type);
                ui.Close();
            }
        }
    }
}
