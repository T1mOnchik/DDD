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
    [SerializeField]private List<string> CARD_TEXT;
    [SerializeField]private List<Sprite> SPRITES;
    [SerializeField]private List<bool> IS_ENCOUNTER;
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
    [SerializeField]public int step = 0;
    [HideInInspector]public Animator normisButtonAnimator;
    [HideInInspector]public Animator metalButtonAnimator;
    private GameObject oldCard;
    
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
        StartCoroutine(PlayCardAnimation( anim));
        // StartCoroutine(SpawnCardAfterAnimation());
        // Debug.Log(normisButtonObject.GetComponent<Animator>().runtimeAnimatorController.animationClips[0]);
        // SpawnCard(CARD_TEXT[step], SPRITES[step], IS_ENCOUNTER[step]);
        RandomCard();
        // step++;
    }

    private IEnumerator PlayCardAnimation( string animName){
        // Debug.Log(animName);
        
        yield return new WaitForSeconds(currentCard.GetComponent<Card>().RemoveCardAnimation(animName)); // activating the card sliding animation and waiting for the end of it
        
        if(oldCard != null){ // Destroing previous card
            Destroy(oldCard);
            Debug.Log(oldCard);
            oldCard = null;
            
        }
        currentCard.transform.SetParent(GameObject.Find("TopLayer").transform);
        yield break;
    }
    // private IEnumerator SpawnCardAfterAnimation(){
    //     normisButtonObject.GetComponent<Animator>().SetTrigger("isClicked");
    //     yield return new WaitForSeconds(.runtimeAnimatorController.animationClips[0].length);
    //     yield break;
    //     // setActiveButtons(false);
    // }

    public void RandomCard(){
        // if(currentCard != null) // Destroing previous card
        //     Destroy(currentCard);

        Debug.Log("step: "+step);
        if(moneyProgressBar.current <= 0)
            SpawnCard("Not enough money", moneyImage, false);
        
        else if(moneyProgressBar.current >= 100)
            SpawnCard("Too much money", moneyImage, false);
        
        else if(psycheProgressBar.current <= 0)
            SpawnCard("Yeah, looks like Gregory couldn't take all that. Well, bring a new one then!", psychoImage, false);
        
        else if(psycheProgressBar.current >= 100)
            SpawnCard("Gregory realized that he had been unsure of himself all this time. Now he understands that he is happy the way he is. Bad ending? Who knows...", psychoImage, false);
        
        else if(popularityProgressBar.current <= 0)
            SpawnCard("So another band has sunk into oblivion. Remind me, what was it's name?", popularityImage, false);
        
        else if(popularityProgressBar.current >= 100)
            SpawnCard("A metal band that is too popular will sooner or later turn into a piece of pop. Our fans are leaving us, my lord...", popularityImage, false);
        
        else{
            if(step < CARD_TEXT.Count){
                if(step == 54)
                    UIManager.instance.LaunchActivity(UIManager.Activity.GuitarHero);

                else if(step == 55)
                    UIManager.instance.LaunchActivity(UIManager.Activity.AccelerometerGame);

                else if(step == 56)
                    UIManager.instance.LaunchActivity(UIManager.Activity.SliderGame);
                    
                else
                    SpawnCard(CARD_TEXT[step], SPRITES[step], IS_ENCOUNTER[step]);
            }
            else
            {
                end.SetActive(true);
            }
        }
        step++;
    }

    

    private GameObject SpawnCard(string text, Sprite image, bool isEncounter){ 
        // instantiating card game object
        currentCard = Instantiate(encounterCardPrefab, new Vector3(0,0,0), Quaternion.identity);

        //setting card's parameters
        currentCard.GetComponent<Card>().isEncounter = isEncounter;
        Debug.Log(isEncounter);
        if(isEncounter) setActiveButtons(true);
        if(oldCard == null) setActiveButtons(false);
        else if(!isEncounter && !oldCard.GetComponent<Card>().isEncounter) setActiveButtons(false);
        
        text = text.Replace("@", Environment.NewLine); // @ is an IMPORTANT symbol which says the system to ADD NEW LINE.  
        currentCard.transform.SetParent(GameObject.Find("SpawnPoint").transform);// setting Canvas as a parent object of the card to make it visible on the UI
        currentCard.GetComponentInChildren<Text>().text = text;
        
        currentCard.transform.Find("Card").transform.Find("Art").GetComponent<Image>().sprite = image;
        currentCard.GetComponent<RectTransform>().anchoredPosition = new Vector2(0.5f, 0); // setting position on the center of the Canvas
        return currentCard;
    }

    //  private void SpawnCard(Card card){ 
    //     if(currentCard != null) // Destroing previous card
    //         Destroy(currentCard);

    //     // instantiating card game object
    //     GameObject toCreate = isEncounter ? encounterCardPrefab : answerCardPrefab; 
    //     currentCard = Instantiate(toCreate, new Vector3(0,0,0), Quaternion.identity);

    //     //setting card's parameters
    //     text = text.Replace("@", Environment.NewLine); // @ is an IMPORTANT symbol which says the system to ADD NEW LINE.  
    //     currentCard.GetComponentInChildren<Text>().text = text;
    //     currentCard.transform.Find("Art").GetComponent<Image>().sprite = image;
    //     currentCard.transform.SetParent(GameObject.Find("CurrentCardLayer").transform); // setting Canvas as a parent object of the card to make it visible on the UI
    //     currentCard.GetComponent<RectTransform>().anchoredPosition = new Vector2(0.5f, 0); // setting position on the center of the Canvas
    // }

    public bool setActiveButtons(bool isActive){
        normisButtonObject.SetActive(isActive);
        metalButtonObject.SetActive(isActive);
        Debug.Log("Buttons Activated: " + isActive);
        return isActive;
    }

    //             DO NOT DELETE THIS!!!

    // public IEnumerator SliderEvant()
    // {   
    //     money.SetActive(false);
    //     psyche.SetActive(false);
    //     popularity.SetActive(false);
    //     demonButtonObj.SetActive(false);
    //     angelButtonObj.SetActive(false);
    //     currentCard.SetActive(false);
    //     sliderObj.SetActive(true);
    //     while(sliderCheck == false)
    //     {
    //         yield return null; 
    //     }
    //     Debug.Log("222");
    //     yield break;
    // }
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
    
}
