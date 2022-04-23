using System.Collections.Generic;

namespace ET
{
    [System.Serializable]
    public class LanguagePack
    {
        private List<string> stringKeys = new List<string>();
        public List<string> Keys { get { return stringKeys; } }

        private List<string> strings = new List<string>();
        public List<string> Strings { get { return strings; } }

        public bool AddNewString(string key, string text)
        {
            if (!stringKeys.Contains(key))
            {
                this.stringKeys.Add(key);
                this.strings.Add(text);
                return true;
            }
            else
            {
                Log.Error($"{key} Key with this name already exists");
                return false;
            }
        }

        public string GetString(string key)
        {
            if (stringKeys.Contains(key))
            {
                return strings[stringKeys.IndexOf(key)];
            }
            else
            {
                return "BAD KEY";
            }
        }

        public void ClearAll()
        {
            this.stringKeys.Clear();
            this.strings.Clear();
        }
    }
}