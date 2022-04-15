using System;
using System.Collections.Generic;

namespace ET
{
    public static class WindowId
    {
	    public const string FGUIPackage = "UISport";

        public const string UILoading = "UILoading";
        public const string UILogin = "UILogin";
    }

    public enum UIWindowType
    {
        Normal,    // 可推出界面(UIMainMenu,UIRank等)
        Fixed,     // 固定窗口(UITopBar等)
        PopUp,     // 模式窗口(UIMessageBox, yourPopWindow , yourTipsWindow ......)
    }

    public enum UIWindowShowMode
    {
        DoNothing = 0,
        HideOtherWindow,
        DestoryOtherWindow,
    }
}