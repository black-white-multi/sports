using LitJson;
using System;
using UnityEngine;

namespace ET
{
    public class Localizer : Entity,IAwake
    {
        public static Localizer Instance { get; private set; }

        public bool IsSwitchLanguage = false;
        public bool PackLoaded = false;
        public Language currLanguage = Language.zhCN;
        public LanguagePack loadedLanguagePack = null;
        public string LanguagePackDirectory = "/Res/Localization/";

        public void Awake()
        {
            IsSwitchLanguage = false;
            Instance = this;
        }

        public override void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }

            base.Dispose();

            PackLoaded = false;
            IsSwitchLanguage = false;
            loadedLanguagePack = null;
            Instance = null;
        }
    }
}