using LitJson;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    [ObjectSystem]
    public class LocalizerAwakeSystem : AwakeSystem<Localizer>
    {
        public override void Awake(Localizer self)
        {
            self.Awake();
        }
    }

    /// <summary>
    /// 多语言
    /// </summary>
    [FriendClass(typeof(Localizer))]
	public static class LocalizerSystem
	{
        public static string GetText(this Localizer self, string key)
        {
            if (self.PackLoaded)
            {
                string buffer = self.loadedLanguagePack.GetString(key);

                return buffer;
            }
            else
            {
                return "INIT FAIL";
            }
        }

        public static void LoadLanguageDefault(this Localizer self)
        {
            int language = PlayerPrefs.GetInt("Language", (int)Language.Unknown);
            if (language == (int)Language.Unknown)
            {
                if (Application.systemLanguage == SystemLanguage.Chinese
                    || Application.systemLanguage == SystemLanguage.ChineseSimplified)
                {
                    language = (int)Language.zhCN;
                }
                else if (Application.systemLanguage == SystemLanguage.ChineseTraditional)
                {
                    language = (int)Language.zhTW;
                }
                else if (Application.systemLanguage == SystemLanguage.German)
                {
                    language = (int)Language.deDE;
                }
                else if (Application.systemLanguage == SystemLanguage.German)
                {
                    language = (int)Language.deDE;
                }
                else if (Application.systemLanguage == SystemLanguage.Spanish)
                {
                    language = (int)Language.esES;
                }
                else if (Application.systemLanguage == SystemLanguage.French)
                {
                    language = (int)Language.frFR;
                }
                else if (Application.systemLanguage == SystemLanguage.Italian)
                {
                    language = (int)Language.itIT;
                }
                else if (Application.systemLanguage == SystemLanguage.Japanese)
                {
                    language = (int)Language.jaJP;
                }
                else if (Application.systemLanguage == SystemLanguage.Korean)
                {
                    language = (int)Language.koKR;
                }
                else if (Application.systemLanguage == SystemLanguage.Portuguese)
                {
                    language = (int)Language.ptPT;
                }
                else if (Application.systemLanguage == SystemLanguage.Russian)
                {
                    language = (int)Language.ruRU;
                }
                else
                {
                    language = (int)Language.enUS;
                }
                PlayerPrefs.SetInt("Language", language);
            }

            self.currLanguage = (Language)language;
            self.LoadLanguageFile(self.currLanguage);
        }

        public static void SwitchLanguage(this Localizer self, Language language)
        {
            if (self.IsSwitchLanguage)
                return;

            PlayerPrefs.SetInt("Language", (int)language);
            self.loadedLanguagePack.ClearAll();
            self.loadedLanguagePack = null;
            self.LoadLanguageFile(language);
            //Game.EventSystem.Run(EventIdType.GameRestart,"");
        }

        public static void LoadLanguageFile(this Localizer self, Language language)
        {
            self.currLanguage = language;
            Game.Scene.GetComponent<ResourcesComponent>().LoadBundle("Localization.unity3d");
            self.loadedLanguagePack = self.LoadToPack(language);
            Game.Scene.GetComponent<ResourcesComponent>().UnloadBundle("Localization.unity3d");
        }

        public static LanguagePack LoadToPack(this Localizer self, Language language)
        {
            try
            {
                LanguagePack pk = new LanguagePack();

                GameObject config = (GameObject)ResourcesComponent.Instance.GetAsset("Localization.unity3d", "Localization");
                string configStr = config.Get<TextAsset>(Enum.GetName(typeof(Language), language)).text;

                foreach (string str in configStr.Split(new[] { "\n" }, StringSplitOptions.None))
                {
                    string str2 = str.Trim();
                    if (str2 == "")
                    {
                        continue;
                    }
                    JsonData data = JsonMapper.ToObject(str);
                    pk.AddNewString(data["Key"].ToString(), data["Text"].ToString());
                    Log.Debug(str);
                }

                self.PackLoaded = true;
                return pk;
            }
            catch
            {
                self.PackLoaded = false;
                return null;
            }
        }
    }
}
