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
