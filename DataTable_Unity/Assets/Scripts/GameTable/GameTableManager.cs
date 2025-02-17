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