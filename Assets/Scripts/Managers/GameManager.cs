using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
   
    public static GameManager instance = null;
    private List<Card> cards;
    private GameObject normisButtonObject;
    private GameObject metalButtonObject;
    private Button normisButton;
    private Button metalButton;
    // private GameObject menu;    // ??????????????????????????????????
    private ProgressBarController moneyProgressBar;
    private ProgressBarController psycheProgressBar;
    private ProgressBarController popularityProgressBar;
    private bool isGameOver = false;

    [Header("Cards")]
    [SerializeField]private int randomCardsQuantity; // max: 29

    [Space(15f)]
    [Header("Visual prefabs")]  // UI references
    [SerializeField]private GameObject end;
    [SerializeField]private GameObject encounterCardPrefab;

    [Header("Background color on lose screen")]
    [SerializeField]private Color32 loseBackgroundColor = new Color32(90,90,90,255);

    [Space(15f)]
    
    private GameObject currentCard;
    private GameObject oldCard;
    private Card currentCardModel;
    [SerializeField]public int step = 0;
    [HideInInspector]public Animator normisButtonAnimator;
    [HideInInspector]public Animator metalButtonAnimator;
    
    // Start is called before the first frame update
    void Start()
    {
        
        if(instance != this)
            Destroy(instance);
        if(instance == null)
            instance = this;
            
        // menu = GameObject.Find("MenuLayout");   // ???????????????????????????????????
        cards = new CSVParser().GenerateCardsScenario(CSVParser.Language.english, randomCardsQuantity); //default language(english)
        // GetLanguagePack(0); //default language(english)
    }

    public void InitGame(){ 
        
        normisButtonObject = GameObject.Find("NormisButton");
        metalButtonObject = GameObject.Find("MetalButton");
        moneyProgressBar = GameObject.Find("Money").GetComponent<ProgressBarController>();
        psycheProgressBar = GameObject.Find("Psyche").GetComponent<ProgressBarController>();
        popularityProgressBar = GameObject.Find("Popularity").GetComponent<ProgressBarController>();
        normisButton = normisButtonObject.GetComponent<Button>();
        metalButton = metalButtonObject.GetComponent<Button>();
        normisButton.onClick.AddListener( () => ClickButton(false) );
        metalButton.onClick.AddListener( () => ClickButton(true) );
        normisButtonAnimator = normisButtonObject.GetComponent<Animator>();
        metalButtonAnimator = metalButtonObject.GetComponent<Animator>();
       
        RandomCard();
    }

    public void GetLanguagePack(int languageID){
        CSVParser.Language language;
        switch (languageID){
            case 0:
                language = CSVParser.Language.english;
            break;

            case 1:
                language = CSVParser.Language.russian;
            break;

            default:
                language = CSVParser.Language.english;
            break;
        }
        // cards = new CSVParser().GenerateCardsScenario(language, randomCardsQuantity); //ISSUE - WE REGENERATE SCENARIO EVERY TIME WE CHANGE LANGUAGE
        cards = new CSVParser().SwitchCardsLanguage(language, cards);
        if(step != 0){
            currentCard.GetComponent<CardController>().SetCardInfo(cards[step-1]);
        }
        UIManager.instance.ApplyLanguagePack(language);
    }

    

    private void ClickButton(bool isMetalist){  // reaction on this encounter: step + 1 = normis; step + 2 = metalist 
        normisButton.enabled = false;
        metalButton.enabled = false;
        if(!isMetalist){
            NextCard("MoveToNormis");
            normisButtonAnimator.SetTrigger("isClicked");
            StartCoroutine(onButtonAnimFinished(normisButtonAnimator));
            step ++;
        }
        else{
            step ++;
            metalButtonAnimator.SetTrigger("isClicked");
            StartCoroutine(onButtonAnimFinished(metalButtonAnimator));
            NextCard("MoveToMetal");
            
        }
    }

    private IEnumerator onButtonAnimFinished(Animator animator){
        yield return new WaitForSeconds(0.47f);
        setActiveButtons(false);
    }

    public void NextCard(string anim){
        oldCard = currentCard;
        ChangeProgressBarValues(currentCardModel.moneyImpact, currentCardModel.psycheImpact, currentCardModel.popularityImpact);
        StartCoroutine(PlayCardAnimation(anim)); // WEAK PLACE //      
        RandomCard();                               // POTENTIAL CRASH! //
    }

    private void ChangeProgressBarValues(int moneyImpact, int psycheImpact, int popularityImpact){
        moneyProgressBar.current += moneyImpact;
        psycheProgressBar.current += psycheImpact;
        popularityProgressBar.current += popularityImpact;
    }

    private IEnumerator PlayCardAnimation(string animName){
        yield return new WaitForSeconds(currentCard.GetComponent<CardController>().RemoveCardAnimation(animName)); // activating the card sliding animation and waiting for the end of it
        
        if(oldCard != null){ // Destroing previous card
            Destroy(oldCard);
            oldCard = null;
        }
        currentCard.transform.SetParent(GameObject.Find("TopLayer").transform);
        yield break;
    }

    public void RandomCard(){
        if(isGameOver)
            UIManager.instance.LaunchActivity(UIManager.Activity.Lose);

        if(moneyProgressBar.current <= 0){
            isGameOver = true;
            SpawnCard(true, "money_low");
        }
        else if(moneyProgressBar.current >= 100){
            isGameOver = true;
            SpawnCard(true, "money_high");
        }
        
        else if(psycheProgressBar.current <= 0){
            isGameOver = true;
            SpawnCard(true, "stress_low");
        }
        
        else if(psycheProgressBar.current >= 100){
            isGameOver = true;
            SpawnCard(true, "stress_high");
        }
        
        else if(popularityProgressBar.current <= 0){
            isGameOver = true;
            SpawnCard(true, "popularity_low");
        }
        
        else if(popularityProgressBar.current >= 100){
            isGameOver = true;
            SpawnCard(true, "popularity_high");
        }
        
        else{
            if(step < cards.Count){
                if(cards[step].text == "TEETH GUITAR GAME")
                    UIManager.instance.LaunchActivity(UIManager.Activity.GuitarHero);

                else if(cards[step].text == "HATER GAME")
                    UIManager.instance.LaunchActivity(UIManager.Activity.HaterFight);
                
                else if(cards[step].text == "JUMP GAME")
                    UIManager.instance.LaunchActivity(UIManager.Activity.JumpGame);
                    
                else
                    SpawnCard(false, null);
            }
            else
            {
                end.SetActive(true);
            }
        }
        step++;
        Debug.Log("step: "+step);
    }

    private GameObject SpawnCard(bool isLose, string death){ 
        if(isLose){
            currentCardModel = new CSVParser().GetDefeatCard(death, CSVParser.Language.russian);
            GameObject.Find("Background").GetComponent<Image>().color = loseBackgroundColor;
        }
            
        else
            currentCardModel = cards[step];

        // instantiating card game object
        currentCard = Instantiate(encounterCardPrefab, new Vector3(0,0,0), Quaternion.identity); // spawning card game object prefab

        //setting card's parameters
        currentCard.GetComponent<CardController>().InitCard(currentCardModel); // inflating card gameobject by information from the model (setting UI, activate/disactivate buttons)
        if(oldCard == null) setActiveButtons(false);                                      
        if(currentCardModel.isEncounter){                                      // depending on is card an encounter, we activate/disactivate "fingers" buttons
            setActiveButtons(true);
        }
        else if(!currentCardModel.isEncounter && oldCard && !oldCard.GetComponent<CardController>().card.isEncounter) 
            setActiveButtons(false);
        
        currentCard.GetComponent<RectTransform>().localScale = new Vector3(1f,1f,0f);
        return currentCard;
    }

    private bool setActiveButtons(bool isActive){ // "Fingers" buttons 
        if(isActive)
            Invoke("ActivateButtons", 0.5f);
        normisButtonObject.SetActive(isActive);
        metalButtonObject.SetActive(isActive);
        return isActive;
    }

    public bool OnMinigameFinished(bool result){
        if(result){
            step++;
            RandomCard();
        }
        else{
            RandomCard();
            step++;
        }
        
        Debug.Log("step: "+step);
        return result;
    }
}
