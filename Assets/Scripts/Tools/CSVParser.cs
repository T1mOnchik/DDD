using System;
using System.Collections.Generic;
using UnityEngine;

public class CSVParser
{

    public enum Type{
        intro,  // prologue + tutorial
        random, // random event cards
        plot,
        concert, // concert cards and minigames
        lose,
        user_interface
    }

    public enum Language{
        russian,
        english,
        ukrainian
    }
    
    // Resource files containing cards info
    // private const string PATH = @"D:\Unity\UnityProjects\DDD\Assets\Resources\Scenario";
    private const string PATH = "Scenario";
    private const string INTRO_FILENAME = @"\intro";
    private const string RANDOM_FILENAME = @"\random";
    private const string PLOT_FILENAME = @"\plot";
    private const string CONCERT_FILENAME = @"\concert";
    private const string LOSE_FILENAME = @"\lose";
    private const string INTERFACE_FILENAME = @"\interface";

    private const string RUS_FOLDERNAME = @"\russian";
    private const string ENG_FOLDERNAME = @"\english";
    private const string UA_FOLDERNAME = @"\ukrainian";

    public List<Card> GenerateCardsScenario(Language language, int randomCardsQuantity){
        List<Card> generalList = ConvertCSVToCards(Type.intro, language);
        
        
        // List<Card> plot = ConvertCSVToCards(CardType.plot, language);

        // int countOfRandomsBetweenPlot = (random.Count % 3) % plot.Count; // Plot and random
        // foreach(Card card in plot){
        //     generalList.Add(card);
        //     for(int i = 0; i < countOfRandomsBetweenPlot; i++)
        //         generalList.Add(random[UnityEngine.Random.Range(0, random.Count - 1)]);
        // }

        foreach (Card card in GenerateRandomCardList(language, randomCardsQuantity))
            generalList.Add(card);

        foreach(Card card in ConvertCSVToCards(Type.concert, language))
            generalList.Add(card);

        return generalList;
    }

    public List<Card> SwitchCardsLanguage(Language language, List<Card> oldCards){
        List<Card> changedList = new List<Card>();
        
        foreach(Card card in oldCards){
            changedList.Add(FindCardByIdAndLanguage(card.id, language));
        }
        return changedList;
    }

    public Card GetDefeatCard(string death, Language language){
        List<Card> loseCards = ConvertCSVToCards(Type.lose, language);
        
        foreach (Card card in loseCards)
        {
            if(card.sprite.name == death)
                return card;
        }
        return null; // заглушка: если карту не нашли, выдать запасную типо: "Тебя сбила машина"
    }

    public Dictionary<string, string> GetUserInterfaceStrings(Language language){
        string[] csvLines = GetFile(Type.user_interface, language);
        Dictionary<string, string> values = new Dictionary<string, string>();
        foreach(string s in csvLines){
            string [] data = s.Split(';');
            values.Add(data[0],data[1]);
        }
        return values;
    }

    private Card FindCardByIdAndLanguage(int id, Language language){
        Type cardType;
        if(id < 100) cardType = Type.intro;
        else if(id >= 100 && id < 300) cardType = Type.random;
        else if(id >= 300 && id < 400) cardType = Type.concert;
        else cardType = Type.lose;
        
        string[] csvLines = GetFile(cardType, language);
        foreach(string s in csvLines){
            string [] data = s.Split(';');

            if(data[0] == "Id") continue;

            try{
                if(int.Parse(data[0]) == id)
                    return new Card(s);
            }
            catch(Exception e){
                Debug.LogException(e);
            }   
        }
        return null; // Zaglushku syuda
    }

    private List<Card> ConvertCSVToCards(Type cardType, Language language)
    {
        string[] csvLines = GetFile(cardType, language);
        List<Card> cards = new List<Card>();

        for (int i = 1; i < csvLines.Length; i++)
        {
            Card cardModel = new Card(csvLines[i]);
            cards.Add(cardModel);
        }   
        return cards;
    }

    private string[] GetFile(Type cardType, Language language){
        string path = PATH;

        //Selecting language folder
        if(language == Language.russian)
            path += RUS_FOLDERNAME;
        else if(language == Language.english)
            path += ENG_FOLDERNAME;
        else if(language == Language.ukrainian)
            path += UA_FOLDERNAME;
        else{
            Debug.LogException(new Exception("Can't find " + language + " language folder name"));
            path += ENG_FOLDERNAME;
        }
            
        
        //Selecting scenario part folder
        if(cardType == Type.intro)
            path += INTRO_FILENAME;
        else if(cardType == Type.random)
            path += RANDOM_FILENAME;
        else if(cardType == Type.plot)
            path += PLOT_FILENAME;
        else if(cardType == Type.concert)
            path += CONCERT_FILENAME;
        else if(cardType == Type.lose)
            path += LOSE_FILENAME;
        else if(cardType == Type.user_interface)
            path += INTERFACE_FILENAME;
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
        List<Card> allRandomCards = ConvertCSVToCards(Type.random, language);
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

    private protected void DebugCards(List<Card> cards){ // method for checking cards info convertation from .csv into List<Card> 
        int j = 0;
        foreach (var card in cards)
        { 
            Debug.Log("Card #" + j +
                Environment.NewLine + card.text +
                Environment.NewLine + card.sprite.name + 
                Environment.NewLine + card.moneyImpact +
                Environment.NewLine + card.psycheImpact +
                Environment.NewLine + card.popularityImpact +
                Environment.NewLine + card.isEncounter);
            j++;
        }
    }
}