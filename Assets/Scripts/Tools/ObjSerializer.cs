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

    [System.Serializable]
    [XmlRoot("GameData")]
    public class MySaveData
    {
        public int money;
        public int popularity;
        public int stress;
        public int step;
        public List<int> cardIds;
    }
    

    [System.Serializable]
    [XmlRoot("Settings")]
    public class MySettingsData
    {

    }
    
    public MySaveData MyData = new MySaveData();
    public MySettingsData MySettings = new MySettingsData();
    private const string DATA_FILE_NAME = "GameData.sav";
    private const string SETTINGS_FILE_NAME = "Settings.sav";

    private MySaveData LoadParameters()
    {
        Debug.Log(MyData.step);
        return MyData;
    }

    public void SaveParameters(int money, int popularity, int stress, int step, List<int> cardIds)
    {
        MyData.money = money;
        MyData.popularity = popularity;
        MyData.stress = stress;
        MyData.step = step;
        MyData.cardIds = cardIds;
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
        bf.Serialize(Stream, MyData);
        Stream.Close();
    }

    public void LoadGame()
    {
        if(!File.Exists(DATA_FILE_NAME))
            return;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream Stream = File.Open(DATA_FILE_NAME, FileMode.Open);
        MyData = bf.Deserialize(Stream) as MySaveData;
        Stream.Close();

        LoadParameters();
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
