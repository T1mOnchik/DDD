using System;
using System.Collections.Generic;
using UnityEngine;

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
        int j = 0;
        foreach (var card in cards)
        { 
            Debug.Log("Card #" + j +
                Environment.NewLine + card.text +
                Environment.NewLine + card.spriteName + 
                Environment.NewLine + card.moneyImpact +
                Environment.NewLine + card.psycheImpact +
                Environment.NewLine + card.popularityImpact +
                Environment.NewLine + card.isEncounter);
            j++;
        }
        return cards;
    }

    
}