using UnityEngine;
using System;

[Serializable]
public class Card
{
    public string text;
    public string spriteName;
    public Sprite sprite;
    public int moneyImpact;
    public int psycheImpact;
    public int popularityImpact;
    public bool isEncounter;
    private string defaultSpriteName = "no_image";
    
    public Card(string rowData){
        string [] data = rowData.Split(';');
        try{
            this.text = FormatText(data[0]);
            this.sprite = GetSpriteForCurrentCard(data[1]);
            this.moneyImpact = int.Parse(data[2]);
            this.psycheImpact = int.Parse(data[3]);
            this.popularityImpact = int.Parse(data[4]);
            this.isEncounter = bool.Parse(data[5]);
        }
        catch(Exception e){
            Debug.LogException(e);
        }   
    }

    private Sprite GetSpriteForCurrentCard(string name){
        sprite = Resources.Load<Sprite>("EncounterSprites/" + name) ?? Resources.Load<Sprite>("EncounterSprites/" + defaultSpriteName); // short if null 
        return sprite;
    }

    private string FormatText(string text){
        text = text.Replace("@", Environment.NewLine); // @ is an IMPORTANT symbol which says the system to ADD NEW LINE.  
        return text;
    }
    
}
