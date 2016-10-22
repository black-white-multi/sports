﻿using System;
using Base;
using Model;
using Object = Base.Object;

namespace App
{
	internal static class Program
	{
		private static void Main(string[] args)
		{
			try
			{
				Log.Info("server start........................");

				BsonClassMapRegister.Register();

				Object.ObjectManager.Register("Base", typeof(Game).Assembly);
				Object.ObjectManager.Register("Model", typeof(ErrorCode).Assembly);
				Object.ObjectManager.Register("Controller", DllHelper.GetController());

				StartConfig startConfig = Game.Scene.AddComponent<StartConfigComponent, string[]>(args).MyConfig;
				
				Game.Scene.AddComponent<EventComponent>();
				Game.Scene.AddComponent<TimerComponent>();

				InnerConfig innerConfig = startConfig.Config.GetComponent<InnerConfig>();
				Game.Scene.AddComponent<NetInnerComponent, string, int>(innerConfig.Host, innerConfig.Port);
				Game.Scene.AddComponent<MessageDispatherComponent, string>(startConfig.Options.AppType);

				// 根据不同的AppType添加不同的组件
				OuterConfig outerConfig = startConfig.Config.GetComponent<OuterConfig>();
				switch (startConfig.Options.AppType)
				{
					case AppType.Manager:
						Game.Scene.AddComponent<NetOuterComponent, string, int>(outerConfig.Host, outerConfig.Port);
						Game.Scene.AddComponent<AppManagerComponent>();
						break;
					case AppType.Realm:
						Game.Scene.AddComponent<NetOuterComponent, string, int>(outerConfig.Host, outerConfig.Port);
						Game.Scene.AddComponent<RealmGateAddressComponent>();
						break;
					case AppType.Gate:
						Game.Scene.AddComponent<NetOuterComponent, string, int>(outerConfig.Host, outerConfig.Port);
						Game.Scene.AddComponent<GateSessionKeyComponent>();
						break;
					default:
						throw new Exception($"命令行参数没有设置正确的AppType: {startConfig.Options.AppType}");
				}

				while (true)
				{
					Object.ObjectManager.Update();
				}
			}
			catch (Exception e)
			{
				Log.Error(e.ToString());
			}
		}
	}
}