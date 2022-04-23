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
	/// <summary>
	/// 管理所有UI
	/// </summary>
	[ChildType(typeof(UIWindow))]
	public class UIManage: Entity, IAwake,ILoad
    {
        public const string FGUIPackage = "UISport";

        public const int DESIGN_SCREEN_WIDTH = 1280;
        public const int DESIGN_SCREEN_HEIGHT = 720;

        public static UIManage Instance;

        public Dictionary<WindowId, Type> dicAllWindows = new Dictionary<WindowId, Type>();
        public Dictionary<WindowId, UIWindow> dicShowWindows = new Dictionary<WindowId, UIWindow>();
        //protected Stack<NavigationData> backSequence;
        public UIWindow curNavigationWindow = null;
        public UIWindow lastNavigationWindow = null;

  //      public override void Dispose()
  //      {
  //          if (this.IsDisposed)
  //          {
  //              return;
  //          }

  //          base.Dispose();

  //          foreach (string type in this.dicShowWindows.Keys.ToArray())
  //          {
  //              UIWindow ui;
  //              if (!this.dicShowWindows.TryGetValue(type, out ui))
  //              {
  //                  continue;
  //              }
  //              this.dicShowWindows.Remove(type);
  //              ui.Close();
  //          }

  //          this.dicShowWindows.Clear();
  //          this.dicAllWindows.Clear();
  //      }

  //      public UIWindow ShowUI(string fguiview)
		//{
  //          UIWindow ui = null;
  //          try
  //          {
  //              ui = ReadyToShowBaseWindow(fguiview);
  //              if (ui != null)
  //              {
  //                  RealShowWindow(fguiview, ui);
  //              }
  //          }
  //          catch (Exception e)
  //          {
  //              Log.Debug(e.ToString());
  //          }
		//	return ui;
  //      }

  //      private UIWindow ReadyToShowBaseWindow(string fguiview)
	 //   {
  //          Type view;
  //          this.dicAllWindows.TryGetValue(fguiview, value: out view);
  //          if (view == null)
  //          {
  //              Log.Debug($"不存在UIBaseView: {fguiview}");
  //              throw new Exception($"不存在UIBaseView: {fguiview}");
  //          }

  //          GObject gObject = UIPackage.CreateObject(UIMangage.FGUIPackage, fguiview);
  //          if (gObject == null)
  //          {
  //              Log.Debug($"{UIMangage.FGUIPackage}不存在: {fguiview}");
  //              throw new Exception($"{UIMangage.FGUIPackage}不存在: {fguiview}");
  //          }

  //          UIWindow ui = this.AddChild<UIWindow, GObject>(gObject);
  //          //UIBaseView baseView = ui.AddComponent<UIBaseView>();
  //          UIBaseView baseView = ui.AddComponent(view, false) as UIBaseView;

  //          //if (baseView.mShowMode == UIWindowShowMode.HideOtherWindow)
  //          //{
  //          //    CloseAllShowUI();
  //          //}

  //          gObject.asCom.MakeFullScreen();
  //          GRoot.inst.AddChild(gObject);

  //          return ui;
  //      }

	 //   private void RealShowWindow(string fguiview, UIWindow ui)
	 //   {
  //          if (!this.dicShowWindows.Keys.Contains(fguiview))
  //          {
  //              ui.Show();
  //              this.Add(fguiview, ui);
  //              lastNavigationWindow = curNavigationWindow;
  //              curNavigationWindow = ui;
  //          }
  //          else
  //          {
  //              Log.Debug($"已存在UIBaseView: {fguiview}");
  //              throw new Exception($"已存在UIBaseView: {fguiview}");
  //          }
  //      }

	 //   public void CloseUI(string fguiview)
	 //   {
	 //       Remove(fguiview);
	 //   }

  //      private void Add(string type, UIWindow ui)
		//{
		//	this.dicShowWindows.Add(type, ui);
		//}

  //      private void Remove(string type)
		//{
		//	UIWindow ui;
		//	if (!this.dicShowWindows.TryGetValue(type, out ui))
		//	{
		//		return;
		//	}
  //          this.dicShowWindows.Remove(type);
		//	ui.Close();
		//}

		//public void CloseAllShowUI()
		//{
		//	foreach (string type in this.dicShowWindows.Keys.ToArray())
		//	{
		//		UIWindow ui;
		//		if (!this.dicShowWindows.TryGetValue(type, out ui))
		//		{
		//			continue;
  //              }
  //              this.dicShowWindows.Remove(type);
		//		ui.Close();
		//	}
		//}

  //      public UIWindow Get(string type)
		//{
		//	UIWindow ui;
		//	this.dicShowWindows.TryGetValue(type, out ui);
		//	return ui;
		//}
    }
}