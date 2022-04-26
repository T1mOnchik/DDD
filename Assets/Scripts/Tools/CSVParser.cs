using System;
using System.Collections.Generic;
using UnityEngine;

public class CSVParser
{

    public enum CardType{
        intro,  // prologue + tutorial
        random, // random event cards
        plot,
        concert // concert cards and minigames
    }

    public enum Language{
        russian,
        english
    }
    
    // Resource files containing cards info
    private const string PATH = @"D:\Unity\UnityProjects\DDD\Assets\Resources\Scenario";
    private const string INTRO_FILENAME = @"\intro.csv";
    private const string RANDOM_FILENAME = @"\random.csv";
    private const string PLOT_FILENAME = @"\plot.csv";
    private const string CONCERT_FILENAME = @"\concert.csv";

    private const string RUS_FOLDERNAME = @"\russian";
    private const string ENG_FOLDERNAME = @"\english";

    private List<Card> ConvertCSVToCards(CardType cardType, Language language)
    {
        // string[] csvLines = System.IO.File.ReadAllLines(@"D:\Unity\UnityProjects\DDD\Assets\Resources\script_example\Cards(rus).csv");
        string[] csvLines = GetFile(cardType, language);
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
        //         Environment.NewLine + card.text +
        //         Environment.NewLine + card.spriteName + 
        //         Environment.NewLine + card.moneyImpact +
        //         Environment.NewLine + card.psycheImpact +
        //         Environment.NewLine + card.popularityImpact +
        //         Environment.NewLine + card.isEncounter);
        //     j++;
        // }
        return cards;
    }

    private string[] GetFile(CardType cardType, Language language){
        string path = PATH;

        //Selecting language folder
        if(language == Language.russian)
            path += RUS_FOLDERNAME;
        else if(language == Language.english)
            path += ENG_FOLDERNAME;
        else
            Debug.LogException(new Exception("Can't find " + language + " language folder name"));
        
        //Selecting scenario part folder
        if(cardType == CardType.intro)
            path += INTRO_FILENAME;
        else if(cardType == CardType.random)
            path += RANDOM_FILENAME;
        else if(cardType == CardType.plot)
            path += PLOT_FILENAME;
        else if(cardType == CardType.concert)
            path += CONCERT_FILENAME;
        else
            Debug.LogException(new Exception("Can't find " + cardType + "file name"));

        string[] result = System.IO.File.ReadAllLines(path);
        
        if(result == null)
            Debug.LogException(new Exception(@"Can't find \" + language +"\" + " + cardType + "file"));

        return result; 
    }

    public List<Card> GenerateCardsScenario(Language language){
        List<Card> generalList = ConvertCSVToCards(CardType.intro, language);
        
        List<Card> random = ConvertCSVToCards(CardType.random, language);
        // List<Card> plot = ConvertCSVToCards(CardType.plot, language);

        // int countOfRandomsBetweenPlot = (random.Count % 3) % plot.Count; // Plot and random
        // foreach(Card card in plot){
        //     generalList.Add(card);
        //     for(int i = 0; i < countOfRandomsBetweenPlot; i++)
        //         generalList.Add(random[UnityEngine.Random.Range(0, random.Count - 1)]);
        // }

        foreach(Card card in random)  // Just Random no plot
            generalList.Add(card);
        
        foreach(Card card in ConvertCSVToCards(CardType.concert, language))
            generalList.Add(card);

        // Debug.Log(generalList);
        return generalList;
    }

}