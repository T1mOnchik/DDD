using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public class EventCardModel
{

    private string text;
    private Sprite sprite;
    private bool isAnswer; 
    private int moneyImpact;
    private int psycheImpact;
    private int popularityImpact;   

    public EventCardModel(string text, Sprite sprite, bool isAnswer, int moneyImpact, int psycheImpact, int popularityImage){
        this.text = text;
        this.sprite = sprite;
        this.isAnswer = isAnswer;
        this.moneyImpact = moneyImpact;
        this.psycheImpact = psycheImpact;
        this.popularityImpact = popularityImage;
    }

    // [Serializable]
    // public class Decision{

    //     public int moneyImpact;
    //     public int psycheImpact;
    //     public int popularityImpact;

    //     public Decision(int moneyImpact, int psycheImpact, int popularityImpact){
    //         this.moneyImpact = moneyImpact;
    //         this.psycheImpact = psycheImpact;
    //         this.popularityImpact = popularityImpact;
    //     }
    // }

}
