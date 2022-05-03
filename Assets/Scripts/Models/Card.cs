using UnityEngine;
using System;

[Serializable]
public class Card
{
    public string text;
    public Sprite sprite;
    public int moneyImpact;
    public int psycheImpact;
    public int popularityImpact;
    public bool isEncounter;
    private const string DEFAULT_SPRITE_NAME = "no_image";
    
    public Card(string rowData){
        string [] data = rowData.Split(';');
        try{
            this.text = FormatText(data[0]);
            this.sprite = GetSpriteForCurrentCard(data[1]);
            if(data[2] == null || data[2] == "") data[2] = "0";
            if(data[3] == null || data[3] == "") data[3] = "0";
            if(data[4] == null || data[4] == "") data[4] = "0";
            this.moneyImpact = int.Parse(data[2]);
            this.psycheImpact = int.Parse(data[3]);
            this.popularityImpact = int.Parse(data[4]);
            this.isEncounter = bool.Parse(data[5]);
        }
        catch(Exception e){
            Debug.LogException(e);
            Debug.Log(data[0]);
        }   
    }

    private Sprite GetSpriteForCurrentCard(string name){
        sprite = Resources.Load<Sprite>("EncounterSprites/" + name) ?? Resources.Load<Sprite>("EncounterSprites/" + DEFAULT_SPRITE_NAME); // short if null 
        return sprite;
    }

    private string FormatText(string text){            // Here we add line breaks to the string
        text = text.Replace("@", Environment.NewLine); // @ = ADD NEW LINE.  
        return text;
    }
    
}
