using FairyGUI;
using UnityEngine;

namespace ET
{
    [ObjectSystem]
    public class UILoginAwakeSystem : AwakeSystem<UILogin>
    {
        public override void Awake(UILogin self)
        {
            self.Awake();
        }
    }

#pragma warning disable ET00003 // 实体类限制多层继承
    [UIView(WindowId.UILogin)]
    public class UILogin : UIBaseView
    {
        private GButton btn_login;
        private GButton btn_play;

        public override void InitUI()
        {
            base.InitUI();
            this.mShowMode = UIWindowShowMode.HideOtherWindow;

            GTextField nametxt = mPanel.GetChild("txt_version").asTextField;
            nametxt.text = $"v0.0.1";

            btn_login = mPanel.GetChild("btn_login").asButton;
            btn_login.title = $"变更使用者";
            //btn_login.title = Localizer.Instance.GetText("GLOBAL_BTN_VISITOR");
            btn_login.onClick.Set(() =>
            {
                Log.Debug($"1");
            });

            btn_play = mPanel.GetChild("btn_play").asButton;
            btn_play.title = $"Name";
            //btn_play.title = Localizer.Instance.GetText("GLOBAL_BTN_ACCOUNT");
            btn_play.onClick.Set(() =>
            {
                Log.Debug($"2");
            });
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
#pragma warning restore ET00003 // 实体类限制多层继承

}
