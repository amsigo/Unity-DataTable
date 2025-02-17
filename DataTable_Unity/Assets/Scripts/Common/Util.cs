public static class Util
{
    public static string GetLocalText(string key)
    {
        if (GameTableManager.Instance.LocalizingTable == null)
            return key;
        
        return GameTableManager.Instance.LocalizingTable.GetLocalizingText(key);
    }

    public static string GetLocalText(string key, LanguageType languageType)
    {
        if (GameTableManager.Instance.LocalizingTable == null)
            return key;

        return GameTableManager.Instance.LocalizingTable.GetLocalizingText(key, languageType);
    }
}