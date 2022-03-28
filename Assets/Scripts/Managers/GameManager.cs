using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
   
    public static GameManager instance = null;
    public GameObject sliderObject;

    [Header("Database simulatation")] // Database simulatation
    // [SerializeField]private List<string> CARD_TEXT;
    // [SerializeField]private List<Sprite> SPRITES;
    // [SerializeField]private List<bool> IS_ENCOUNTER;
    [SerializeField]private List<Card> cards;

    [Header("Visuals")]  // UI references
    [SerializeField]private Sprite moneyImage;
    [SerializeField]private Sprite psychoImage;
    [SerializeField]private Sprite popularityImage;
    //    [SerializeField]private GameObject sliderObj;
    [SerializeField]private GameObject end;
    [SerializeField]private GameObject encounterCardPrefab;
    [SerializeField]private GameObject answerCardPrefab;
    private GameObject normisButtonObject;
    private GameObject metalButtonObject;
    private Button normisButton;
    private Button metalButton;
    private ProgressBarController moneyProgressBar;
    private ProgressBarController psycheProgressBar;
    private ProgressBarController popularityProgressBar;

    [Space(15f)]

    public bool sliderCheck = false;
    
    private GameObject currentCard;
    private GameObject oldCard;
    private Card currentCardModel;
    [SerializeField]public int step = 0;
    [HideInInspector]public Animator normisButtonAnimator;
    [HideInInspector]public Animator metalButtonAnimator;
    
    
    [SerializeField]private bool sliderGameResult;
    // Start is called before the first frame update
    void Start()
    {
        if(instance != this)
            Destroy(instance);
        if(instance == null)
            instance = this;

        cards = new CSVParser().ConvertCSVToCards();
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

    private void ClickButton(bool isMetalist){  // answers for this encounter: step + 1 = normis; step + 2 = metalist 
        normisButton.enabled = false;
        metalButton.enabled = false;
        if(!isMetalist){    // NU OCHEVIDNO ETO TUPO KOSTYLYOK NADA ISPRAVIT
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
        StartCoroutine(PlayCardAnimation(anim));
        RandomCard();
        ChangeProgressBarValues(currentCardModel.moneyImpact, currentCardModel.psycheImpact, currentCardModel.popularityImpact);
    }

    private void ChangeProgressBarValues(int moneyImpact, int psycheImpact, int popularityImpact){
        moneyProgressBar.current -= moneyImpact;
        psycheProgressBar.current -= psycheImpact;
        popularityProgressBar.current -= popularityImpact;
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
        if(moneyProgressBar.current <= 0)
            // SpawnCard("Not enough money", moneyImage, false);
            end.SetActive(true);
        
        else if(moneyProgressBar.current >= 100)
            // SpawnCard("Too much money", moneyImage, false);
            end.SetActive(true);
        
        else if(psycheProgressBar.current <= 0)
            // SpawnCard("Yeah, looks like Gregory couldn't take all that. Well, bring a new one then!", psychoImage, false);
            end.SetActive(true);
        
        else if(psycheProgressBar.current >= 100)
            // SpawnCard("Gregory realized that he had been unsure of himself all this time. Now he understands that he is happy the way he is. Bad ending? Who knows...", psychoImage, false);
            end.SetActive(true);
        
        else if(popularityProgressBar.current <= 0)
            // SpawnCard("So another band has sunk into oblivion. Remind me, what was it's name?", popularityImage, false);
            end.SetActive(true);
        
        else if(popularityProgressBar.current >= 100)
            // SpawnCard("A metal band that is too popular will sooner or later turn into a piece of pop. Our fans are leaving us, my lord...", popularityImage, false);
            end.SetActive(true);
        
        else{
            if(step < cards.Count){
                if(step == 106)
                    UIManager.instance.LaunchActivity(UIManager.Activity.GuitarHero);

                // else if(step == 55)
                //     UIManager.instance.LaunchActivity(UIManager.Activity.AccelerometerGame);

                else if(step == 110)
                    UIManager.instance.LaunchActivity(UIManager.Activity.HaterFight);
                
                else if(step == 116)
                    UIManager.instance.LaunchActivity(UIManager.Activity.JumpGame);
                    
                else
                    SpawnCard();
            }
            else
            {
                end.SetActive(true);
            }
        }
        step++;
        Debug.Log("step: "+step);
    }

    private GameObject SpawnCard(){ 
        currentCardModel = cards[step];
        // instantiating card game object
        currentCard = Instantiate(encounterCardPrefab, new Vector3(0,0,0), Quaternion.identity);

        //setting card's parameters
        currentCard.GetComponent<CardController>().SetCardInfo(currentCardModel);
        if(oldCard == null) setActiveButtons(false);
        if(currentCardModel.isEncounter){
            setActiveButtons(true);
        }
            
        else if(!currentCardModel.isEncounter && !oldCard.GetComponent<CardController>().card.isEncounter) setActiveButtons(false);
        return currentCard;
    }

    private bool setActiveButtons(bool isActive){
        if(isActive)
            Invoke("ActivateButtons", 0.5f);
        normisButtonObject.SetActive(isActive);
        metalButtonObject.SetActive(isActive);
        // Debug.Log("Buttons Activated: " + isActive);
        return isActive;
    }

    private void ActivateButtons(){
        normisButton.enabled = true;
        metalButton.enabled = true;
    }

    public IEnumerator SliderGame()
    {   
        SliderController sliderController = sliderObject.GetComponent<SliderController>();
        sliderObject.SetActive(true);
        while(!sliderController.isGameEnd)
        {
            yield return null;
        }
        sliderGameResult = sliderController.result;
        sliderObject.SetActive(false);
        Debug.Log(sliderGameResult);
        yield break;
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
