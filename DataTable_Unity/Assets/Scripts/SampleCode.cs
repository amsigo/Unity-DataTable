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