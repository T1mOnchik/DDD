using System;
using System.Collections.Generic;
using UnityEngine;

public class CSVParser
{

    public enum CardType{
        intro,  // prologue + tutorial
        random, // random event cards
        plot,
        concert, // concert cards and minigames
        lose
    }

    public enum Language{
        russian,
        english
    }
    
    // Resource files containing cards info
    // private const string PATH = @"D:\Unity\UnityProjects\DDD\Assets\Resources\Scenario";
    private const string PATH = "Scenario";
    private const string INTRO_FILENAME = @"\intro";
    private const string RANDOM_FILENAME = @"\random";
    private const string PLOT_FILENAME = @"\plot";
    private const string CONCERT_FILENAME = @"\concert";
    private const string LOSE_FILENAME = @"\lose";

    private const string RUS_FOLDERNAME = @"\russian";
    private const string ENG_FOLDERNAME = @"\english";

    private List<Card> ConvertCSVToCards(CardType cardType, Language language)
    {
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
        else if(cardType == CardType.lose)
            path += LOSE_FILENAME;
        else
            Debug.LogException(new Exception("Can't find " + cardType + "file name"));

        string[] result;
        
        TextAsset csv = (TextAsset)Resources.Load(path, typeof(TextAsset));
        result = csv.text.Split('\n');
            
        if(result == null)
            Debug.LogException(new Exception(@"Can't find \" + language +"\" + " + cardType + "file"));

        return result; 
    }

    private List<Card> GenerateRandomCardList(Language language, int quantity){
        List<Card> selectedRandomCards = new List<Card>();
        List<Card> allRandomCards = ConvertCSVToCards(CardType.random, language);
        List<int> encounterCardsIndexes = new List<int>();

        for(int i = 0; i < allRandomCards.Count; i++)
            if(allRandomCards[i].isEncounter)
                encounterCardsIndexes.Add(i);

        if(quantity > encounterCardsIndexes.Count)
            quantity = encounterCardsIndexes.Count;

        for(int i = 0; i < quantity; i++){
            int randomIndex = UnityEngine.Random.Range(0, encounterCardsIndexes.Count - 1);
            int cardToAddIndex = encounterCardsIndexes[randomIndex]; //getting random index from list of encounterCards 
            encounterCardsIndexes.RemoveAt(randomIndex);

            selectedRandomCards.Add(allRandomCards[cardToAddIndex]);       // adding encounter 
            selectedRandomCards.Add(allRandomCards[cardToAddIndex + 1]);   // adding normis answer
            selectedRandomCards.Add(allRandomCards[cardToAddIndex + 2]);   // adding metal answer
        }   
        return selectedRandomCards;
    }

    public List<Card> GenerateCardsScenario(Language language, int randomCardsQuantity){
        List<Card> generalList = ConvertCSVToCards(CardType.intro, language);
        
        
        // List<Card> plot = ConvertCSVToCards(CardType.plot, language);

        // int countOfRandomsBetweenPlot = (random.Count % 3) % plot.Count; // Plot and random
        // foreach(Card card in plot){
        //     generalList.Add(card);
        //     for(int i = 0; i < countOfRandomsBetweenPlot; i++)
        //         generalList.Add(random[UnityEngine.Random.Range(0, random.Count - 1)]);
        // }

        foreach (Card card in GenerateRandomCardList(language, randomCardsQuantity))
            generalList.Add(card);

        foreach(Card card in ConvertCSVToCards(CardType.concert, language))
            generalList.Add(card);

        // Debug.Log(generalList);
        return generalList;
    }

    public Card GetDefeatCard(string death, Language language){
        List<Card> loseCards = ConvertCSVToCards(CardType.lose, language);
        
        foreach (Card card in loseCards)
        {
            if(card.sprite.name == death)
                return card;
        }
        Debug.Log("exit");
        return null; // заглушка: если карту не нашли, выдать запасную типо: "Тебя сбила машина"
    }

}