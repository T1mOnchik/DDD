using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public class EventCardModel
{

    public string encounterText;
    public string normisText;
    public string metalText;
    // public Sprite sprite;
    public string eSpriteName;
    public string nSpriteName;
    public string mSpriteName;
    public float moneyImpactNormis;
    public float moneyImpactMetal;
    public float psycheImpactNormis;
    public float psycheImpactMetal;
    public float popularityImpactNormis;
    public float popularityImpactMetal;
    public bool isAnswer;
    
    public EventCardModel(string rowData){
        string [] data = rowData.Split(';');
        
        this.encounterText = data[0]; 
        this.eSpriteName = data[1];
        this.normisText = data[2];
        this.nSpriteName = data[3];
        this.metalText = data[4];
        this.mSpriteName = data[5];
        this.moneyImpactNormis = float.Parse(data[6]);
        this.psycheImpactNormis = float.Parse(data[7]);
        this.popularityImpactNormis = float.Parse(data[8]);
        this.moneyImpactMetal = float.Parse(data[9]);
        this.psycheImpactMetal = float.Parse(data[10]);
        this.popularityImpactMetal = float.Parse(data[11]);
    }

}
