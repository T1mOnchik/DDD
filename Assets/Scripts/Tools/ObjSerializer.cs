using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class ObjSerializer 
{
    //
    //                                                                  /
    //                                                                 /
    //                                                              
    // ПОМЕНЯЙ ФАЙЛОВУЮ СИСТЕМУ ТИ ВІБЛЯДОК СУКА ЕБАНІЙ            //
    //                                                             //
    //                                                             //
    //                                                          ///////

    
    private GameSaveData gameSaveData;

    [System.Serializable]
    [XmlRoot("Settings")]
    public class MySettingsData
    {

    }
    
    // public MySaveData MyData = new MySaveData();
    public MySettingsData MySettings = new MySettingsData();
    private const string DATA_FILE_NAME = "GameData.sav";
    private const string SETTINGS_FILE_NAME = "Settings.sav";

    // private GameSaveData LoadParameters()
    // {   
    //     return gameSaveData;
    // }

    public void SaveParameters(int money, int popularity, int stress, int step, List<int> cardIds)
    {
        gameSaveData = new GameSaveData(money, popularity, stress, step, cardIds);
    }

    public void SaveGame(int money, int popularity, int stress, int step, List<int> cardIds)
    {   
        if(File.Exists(DATA_FILE_NAME))
        {
            File.Delete(DATA_FILE_NAME);
        }
        SaveParameters(money, popularity, stress, step, cardIds);

        BinaryFormatter bf = new BinaryFormatter();
        FileStream Stream = File.Create(DATA_FILE_NAME);
        bf.Serialize(Stream, gameSaveData);
        Stream.Close();
    }

    public GameSaveData LoadGame()
    {
        if(!File.Exists(DATA_FILE_NAME))
            return null;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream Stream = File.Open(DATA_FILE_NAME, FileMode.Open);
        gameSaveData = bf.Deserialize(Stream) as GameSaveData;
        Stream.Close();

        return gameSaveData;
    }

    private MySettingsData LoadSettingsData()
    {
        return MySettings;
    }

    public void SaveSettingsData()
    {

    }

    public void SaveSettings()
    {

    }

    public void LoadSettings()
    {

    }

}
