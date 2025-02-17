using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LanguageType
{
    KOR,
    ENG,
    JP
}

public class LocalizingData
{
    public string key;
    public string kor;
    public string eng;
    public string jp;
}

[System.Serializable]
public class LocalizingTableRow
{
    public string key;
    public string kor;
    public string eng;
    public string jp;
}

public class LocalizingTable : BaseTable
{
    LocalizingTableRow[] localizingTableRows;
    Dictionary<string, LocalizingData> localizingDataDict = new Dictionary<string, LocalizingData>();

    public override void Parsing(string jsonPath)
    {
        base.Parsing(jsonPath);

        localizingTableRows = JsonHelper.FromJson<LocalizingTableRow>(json);
        localizingDataDict = ConvertListToDict();
    }

    Dictionary<string, LocalizingData> ConvertListToDict()
    {
        Dictionary<string, LocalizingData> dict = new Dictionary<string, LocalizingData>();

        foreach(var row in localizingTableRows)
        {
            string key = row.key;

            LocalizingData localizingData = new LocalizingData();
            localizingData.key = row.key;
            localizingData.kor = row.kor;
            localizingData.eng = row.eng;
            localizingData.jp = row.jp;

            if (!dict.ContainsKey(key))
                dict.Add(key, localizingData);
        }

        return dict;
    }

    public string GetLocalizingText(string key)
    {
        if (Application.systemLanguage == SystemLanguage.Korean)
            return GetLocalizingText(key, LanguageType.KOR);

        else if (Application.systemLanguage == SystemLanguage.English)
            return GetLocalizingText(key, LanguageType.KOR);

        else if (Application.systemLanguage == SystemLanguage.Japanese)
            return GetLocalizingText(key, LanguageType.KOR);
        else
            return GetLocalizingText(key, LanguageType.ENG);
    }

    public string GetLocalizingText(string key, LanguageType languageType)
    {
        switch(languageType)
        {
            case LanguageType.KOR:
                return localizingDataDict[key].kor;

            case LanguageType.ENG:
                return localizingDataDict[key].eng;

            case LanguageType.JP:
                return localizingDataDict[key].jp;

            default:
                return localizingDataDict[key].kor;
        }
    }
}