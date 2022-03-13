using System;
using System.Collections.Generic;

public class CSVParser
{

    public List<Card> ConvertCSVToCards()
    {
        string[] csvLines = System.IO.File.ReadAllLines(@"D:\Unity\UnityProjects\DDD\Assets\Resources\script_example\example.csv");
        List<Card> cards = new List<Card>();

        for (int i = 1; i < csvLines.Length; i++)
        {
            Card cardModel = new Card(csvLines[i]);
            cards.Add(cardModel);
        }   
        // int j = 0;
        // foreach (var card in cards)
        // { 
        //     Debug.Log("Card #" + j +
        //         Environment.NewLine + card.encounterText +
        //         Environment.NewLine + card.eSpriteName + 
        //         Environment.NewLine + card.normisText +
        //         Environment.NewLine + card.nSpriteName +
        //         Environment.NewLine + card.metalText +
        //         Environment.NewLine + card.mSpriteName +
        //         Environment.NewLine + card.moneyImpactNormis +
        //         Environment.NewLine + card.psycheImpactNormis +
        //         Environment.NewLine + card.popularityImpactNormis +
        //         Environment.NewLine + card.moneyImpactMetal +
        //         Environment.NewLine + card.psycheImpactMetal +
        //         Environment.NewLine + card.popularityImpactMetal);
        //     j++;
        // }
        return cards;
    }

    
}