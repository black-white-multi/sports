using FairyGUI;

namespace ET
{

    [ObjectSystem]
    public class UIWindowAwakeSystem : AwakeSystem<UIWindow, GObject>
    {
        public override void Awake(UIWindow self, GObject gobject)
        {
            self.WindowData = self.AddChild<WindowCoreData>();
            self.mGObject = gobject;
            self.mPanel = self.mGObject?.asCom;
        }
    }

    [FriendClass(typeof(UIWindow))]
    public static class UIWindowSystem
    {
        public static void Show(this UIWindow self)
        {
            self.mGObject?.asCom.MakeFullScreen();
#pragma warning disable ET00001 // AddChild方法类型约束错误
            GRoot.inst.AddChild(self.mGObject);
#pragma warning restore ET00001 // AddChild方法类型约束错误
        }

        public static void Close(this UIWindow self)
        {
            self.mGObject?.Dispose();
            self.Dispose();
        }
    }
}
