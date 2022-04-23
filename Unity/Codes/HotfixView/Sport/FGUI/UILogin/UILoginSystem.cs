using FairyGUI;

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

    [FriendClass(typeof(UIWindow))]
    [FriendClass(typeof(WindowCoreData))]
    [FriendClass(typeof(UILogin))]
    public static class UILoginSystem
    {
        public static void Awake(this UILogin self) 
        {
            UIWindow uiWindow = self.Parent as UIWindow;
            uiWindow.WindowData.windowType = UIWindowType.Normal;

            self.InitUI();
        }

        public static void InitUI(this UILogin self)
        {
            UIWindow uiWindow = self.Parent as UIWindow;
            GTextField nametxt = uiWindow.mPanel.GetChild("txt_version").asTextField;
            nametxt.text = $"v0.0.1";

            self.btn_login = uiWindow.mPanel.GetChild("btn_login").asButton;
            self.btn_login.title = Localizer.Instance.GetText("GLOBAL_BTN_VISITOR");
            self.btn_login.onClick.Set(() =>
            {
                //UIManage.Instance.CloseAllShowUI();
            });

            self.btn_play = uiWindow.mPanel.GetChild("btn_play").asButton;
            self.btn_play.title = $"Name";
            //btn_play.title = Localizer.Instance.GetText("GLOBAL_BTN_ACCOUNT");
            self.btn_play.onClick.Set(() =>
            {
                Log.Debug($"2");
            });

            self.btn_language = uiWindow.mPanel.GetChild("btn_language").asButton;
            self.btn_language.title = $"简体中文";

            SoundComponent soundComponent = Game.Scene.GetComponent<SoundComponent>();
            self.btn_sound = uiWindow.mPanel.GetChild("btn_sound").asButton;
            self.btn_sound.onClick.Set(() => {
                soundComponent.SoundMute = !soundComponent.SoundMute;
                if (soundComponent.SoundMute)
                {
                    GRoot.inst.soundVolume = 0;
                }
                else
                {
                    GRoot.inst.soundVolume = 1f;
                }
                self.RefreshButtonsState();
            });
            self.RefreshButtonsState();
        }

        public static void RefreshButtonsState(this UILogin self)
        {
            SoundComponent soundComponent = Game.Scene.GetComponent<SoundComponent>();
            GLoader sound_state = self.btn_sound.GetChild("icon").asLoader;
            sound_state.url = $"ui://UISport/icon_sound_off";
            if (soundComponent.SoundMute)
            {
                sound_state.url = $"ui://UISport/icon_sound_off";
            }
            else
            {
                sound_state.url = $"ui://UISport/icon_sound_on";
            }
        }
    }
}
