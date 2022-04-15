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
        private GButton btn_account;
        private GButton btn_visitor;

        public override void InitUI()
        {
            base.InitUI();
            this.mShowMode = UIWindowShowMode.HideOtherWindow;

            GTextField nametxt = mPanel.GetChild("txt_version").asTextField;
            nametxt.text = $"v0.0.1";

            btn_visitor = mPanel.GetChild("btn_visitor").asButton;
            //btn_visitor.title = Localizer.Instance.GetText("GLOBAL_BTN_VISITOR");
            btn_visitor.onClick.Set(() =>
            {
                Log.Debug($"1");
            });

            btn_account = mPanel.GetChild("btn_account").asButton;
            //btn_account.title = Localizer.Instance.GetText("GLOBAL_BTN_ACCOUNT");
            btn_account.onClick.Set(() =>
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
