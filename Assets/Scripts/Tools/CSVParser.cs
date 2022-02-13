using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVParser
{

    public void ParseCSVToCards()
    {
        string[] csvLines = System.IO.File.ReadAllLines(@"D:\Unity\UnityProjects\DDD\Assets\Resources\script_example\example.csv");
        List<EventCardModel> cards = new List<EventCardModel>();

        for (int i = 1; i < csvLines.Length; i++)
        {
            EventCardModel cardModel = new EventCardModel(csvLines[i]);
            cards.Add(cardModel);
        }   
        // int j = 0;
        // foreach (var card in cards)
        // { 
        //     Debug.Log("Card #" + j);
        //     Debug.Log(card.encounterText);
        //     Debug.Log(card.eSpriteName);
        //     Debug.Log(card.normisText);
        //     Debug.Log(card.nSpriteName);
        //     Debug.Log(card.metalText);
        //     Debug.Log(card.mSpriteName);
        //     Debug.Log(card.moneyImpactNormis);
        //     Debug.Log(card.psycheImpactNormis);
        //     Debug.Log(card.popularityImpactNormis);
        //     Debug.Log(card.moneyImpactMetal);
        //     Debug.Log(card.psycheImpactMetal);
        //     Debug.Log(card.popularityImpactMetal);
        //     j++;
        // }
    }

    
}
