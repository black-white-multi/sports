using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FairyGUI;
using FairyGUI.Utils;
using UnityEngine;
//using UnityEngine.Rendering.Universal;

namespace ET
{
	[ObjectSystem]
	public class UIMangageAwakeSystem : AwakeSystem<UIMangage>
	{
		public override void Awake(UIMangage self)
		{
			self.Awake();
		}
	}

	[ObjectSystem]
	public class UIMangageLoadSystem : LoadSystem<UIMangage>
	{
		public override void Load(UIMangage self)
		{
			self.Load();
		}
	}

	/// <summary>
	/// 管理所有UI
	/// </summary>
	public class UIMangage: Entity, IAwake,ILoad
    {
        public const int DESIGN_SCREEN_WIDTH = 1280;
        public const int DESIGN_SCREEN_HEIGHT = 720;

        public static UIMangage Instance;

        private readonly Dictionary<string, Type> dicAllWindows = new Dictionary<string, Type>();
		private readonly Dictionary<string, UIWindow> dicShowWindows = new Dictionary<string, UIWindow>();
	    //protected Stack<NavigationData> backSequence;
	    protected UIWindow curNavigationWindow = null;
	    protected UIWindow lastNavigationWindow = null;

        public void Awake()
		{
		    Instance = this;

            //设置屏幕常亮
            Screen.sleepTimeout = SleepTimeout.NeverSleep;

            string path = "FGUI/";
            //加载UI包
            UIPackage.AddPackage($"{path}{WindowId.FGUIPackage}");

            FontManager.RegisterFont(FontManager.GetFont("Myriad-Bold"), "Myriad-Bold");
            UIConfig.defaultFont = "Myriad-Bold";

            GRoot.inst.SetContentScaleFactor(DESIGN_SCREEN_WIDTH, DESIGN_SCREEN_HEIGHT, UIContentScaler.ScreenMatchMode.MatchWidthOrHeight);
            this.Load();

            GameObject.DontDestroyOnLoad(StageCamera.main);
            //UniversalAdditionalCameraData uacd = StageCamera.main.GetUniversalAdditionalCameraData();
            //uacd.renderType = CameraRenderType.Overlay;
        }

		public void Load()
		{
			this.dicAllWindows.Clear();

            Type[] types = CodeLoader.Instance.GetTypes();

            foreach (Type type in types)
			{
				object[] attrs = type.GetCustomAttributes(typeof (UIViewAttribute), false);
				if (attrs.Length == 0)
				{
					continue;
				}

			    UIViewAttribute attribute = attrs[0] as UIViewAttribute;
				if (this.dicAllWindows.ContainsKey(attribute.Type))
				{
                    Log.Debug($"已经存在同类UIBaseView: {attribute.Type}");
					throw new Exception($"已经存在同类UIBaseView: {attribute.Type}");
				}
                //object o = Activator.CreateInstance(type);
                //UIBaseView baseView = o as UIBaseView;
                //if (baseView == null)
                //{
                //	Log.Error($"{o.GetType().FullName} 没有继承 UIBaseView");
                //	continue;
                //}
                this.dicAllWindows.Add(attribute.Type, type);
			}
		}

        public override void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }

            base.Dispose();

            foreach (string type in this.dicShowWindows.Keys.ToArray())
            {
                UIWindow ui;
                if (!this.dicShowWindows.TryGetValue(type, out ui))
                {
                    continue;
                }
                this.dicShowWindows.Remove(type);
                ui.Close();
            }

            this.dicShowWindows.Clear();
            this.dicAllWindows.Clear();
        }

        public UIWindow ShowUI(string fguiview)
		{
            UIWindow ui = null;
            try
            {
                ui = ReadyToShowBaseWindow(fguiview);
                if (ui != null)
                {
                    RealShowWindow(fguiview, ui);
                }
            }
            catch (Exception e)
            {
                Log.Debug(e.ToString());
            }
			return ui;
        }

        private UIWindow ReadyToShowBaseWindow(string fguiview)
	    {
            Type view;
            this.dicAllWindows.TryGetValue(fguiview, value: out view);
            if (view == null)
            {
                Log.Debug($"不存在UIBaseView: {fguiview}");
                throw new Exception($"不存在UIBaseView: {fguiview}");
            }

            GObject gObject = UIPackage.CreateObject(WindowId.FGUIPackage, fguiview);
            if (gObject == null)
            {
                Log.Debug($"{WindowId.FGUIPackage}不存在: {fguiview}");
                throw new Exception($"{WindowId.FGUIPackage}不存在: {fguiview}");
            }

            UIWindow ui = this.AddChild<UIWindow, GObject>(gObject);
            //UIBaseView baseView = ui.AddComponent<UIBaseView>();
            UIBaseView baseView = ui.AddComponent(view, false) as UIBaseView;

            if (baseView.mShowMode == UIWindowShowMode.HideOtherWindow)
            {
                CloseAllShowUI();
            }

            gObject.asCom.MakeFullScreen();
            GRoot.inst.AddChild(gObject);

            return ui;
        }

	    private void RealShowWindow(string fguiview, UIWindow ui)
	    {
            if (!this.dicShowWindows.Keys.Contains(fguiview))
            {
                ui.Show();
                this.Add(fguiview, ui);
                lastNavigationWindow = curNavigationWindow;
                curNavigationWindow = ui;
            }
            else
            {
                Log.Debug($"已存在UIBaseView: {fguiview}");
                throw new Exception($"已存在UIBaseView: {fguiview}");
            }
        }

	    public void CloseUI(string fguiview)
	    {
	        Remove(fguiview);
	    }

        private void Add(string type, UIWindow ui)
		{
			this.dicShowWindows.Add(type, ui);
		}

        private void Remove(string type)
		{
			UIWindow ui;
			if (!this.dicShowWindows.TryGetValue(type, out ui))
			{
				return;
			}
            this.dicShowWindows.Remove(type);
			ui.Close();
		}

		public void CloseAllShowUI()
		{
			foreach (string type in this.dicShowWindows.Keys.ToArray())
			{
				UIWindow ui;
				if (!this.dicShowWindows.TryGetValue(type, out ui))
				{
					continue;
                }
                this.dicShowWindows.Remove(type);
				ui.Close();
			}
		}

        public UIWindow Get(string type)
		{
			UIWindow ui;
			this.dicShowWindows.TryGetValue(type, out ui);
			return ui;
		}
    }
}