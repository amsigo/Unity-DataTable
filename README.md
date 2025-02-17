Excel to Json
========================
 
 Repository를 받고 JsonConverter Folder의 template 엑셀 파일을 수정하여 table 폴더에 추가하고 JsonConverter.exe를 실행하면 엑셀 파일이 json파일로 변환되어
 output 폴더에 json 파일로 추출됩니다.
 
 
**[Localizing Excel Table]**
![Image](https://github.com/user-attachments/assets/b7b726e6-5397-408b-99f9-c6d347f20c79)   


**[변환된 Json 파일]**

![Image](https://github.com/user-attachments/assets/f9b22629-a4dc-4e6e-bec6-6ecc8729cd49)   

DataTable Parsing Sample
========================

BaseTable
------------------------
해당 코드는 Table 스크립트의 Base가 되는 코드로 해당 스크립트를 상속하여 테이블 파싱 스크립트를 작성합니다.

```C#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTable
{
    protected string json;

    public virtual void Parsing(string jsonPath)
    {
        json = Resources.Load<TextAsset>(jsonPath).text;
    }

}
```

Sample Table Code
------------------------

```C#
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
```

Use Data Sample
========================
### Data Parsing
아래와 같이 테이블을 Parsing 한 후 작성한 테이블 Script에 접근하여 데이터를 가져옵니다.

```C#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTableManager : Singleton<GameTableManager>
{
    private readonly string gameTablePath = "GameTable";

    private LocalizingTable localizingTable = new LocalizingTable();

    public LocalizingTable LocalizingTable => localizingTable;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        ParsingTable();

        
    }

    void ParsingTable()
    {
        localizingTable.Parsing(gameTablePath + "/Localizing");
    }
}
```

### Util
데이터를 편하게 가져오기 위해 Util 스크립트를 따로 만들어 함수를 구현 하였습니다.
```C#
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
```

### Get Data
실제 사용 예시 입니다.
```C#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleCode : MonoBehaviour
{
    public string[] localTextKeyArray;

    private void Start()
    {
        for (int i = 0; i < localTextKeyArray.Length; i++)
        {
            string key = localTextKeyArray[i];

            string ko = Util.GetLocalText(key, LanguageType.KOR);
            string en = Util.GetLocalText(key, LanguageType.ENG);
            string jp = Util.GetLocalText(key, LanguageType.JP);

            Debug.Log($"key : {key}, ko : {ko}, en : {en}, jp : {jp}");
        }
    }
}
```

### 실행 결과 화면
![Image](https://github.com/user-attachments/assets/d6f18b7f-7527-4495-b79c-66da8bf4595e)
