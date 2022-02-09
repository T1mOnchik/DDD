using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   
    public static GameManager instance = null;
    
    [Header("Database simulatation")] // Database simulatation
    [SerializeField]private List<string> CARD_TEXT;
    [SerializeField]private List<Sprite> SPRITES;
    [SerializeField]private List<bool> IS_ENCOUNTER;
    [SerializeField]private List<EventCardModel> cards;

    [Header("UI")]  // UI references
    [SerializeField]private Sprite moneyImage;
    [SerializeField]private Sprite psychoImage;
    [SerializeField]private Sprite popularityImage;
    //    [SerializeField]private GameObject sliderObj;
    [SerializeField]private GameObject end;
    [SerializeField]private GameObject encounterCardPrefab;
    [SerializeField]private GameObject answerCardPrefab;
    private GameObject canvas;
    private GameObject mainMenu;
    private GameObject normisButtonObject;
    private GameObject metalButtonObject;
    private Button normisButton;
    private Button metalButton;
    private ProgressBarController moneyProgressBar;
    private ProgressBarController psycheProgressBar;
    private ProgressBarController popularityProgressBar;

    [Header("Visual Effects")]    
    [SerializeField]private int fadeSpeed = 2;

    [Space(15f)]


    public bool sliderCheck = false;
    public int guitarHeroScore = 0;
    
    private GameObject currentCard;
    [SerializeField]private int step = 0;
    private enum Activity{
        LoadGame,
        GuitarHero,
        smthElse
    }
    
    
    // Start is called before the first frame update
    void Start()
    {   
        if(instance != this)
            Destroy(instance);
        if(instance == null)
            instance = this;

        
        mainMenu = GameObject.Find("MainMenu");
        mainMenu.GetComponent<Button>().onClick.AddListener( () => StartCoroutine(LaunchActivity(Activity.LoadGame)) );
    }

    private void InitGame(){ 
        canvas = GameObject.Find("Canvas");
        normisButtonObject = GameObject.Find("NormisButton");
        metalButtonObject = GameObject.Find("MetalButton");
        moneyProgressBar = GameObject.Find("Money").GetComponent<ProgressBarController>();
        psycheProgressBar = GameObject.Find("Psyche").GetComponent<ProgressBarController>();
        popularityProgressBar = GameObject.Find("Popularity").GetComponent<ProgressBarController>();
        normisButton = normisButtonObject.GetComponent<Button>();
        metalButton = metalButtonObject.GetComponent<Button>();
        normisButton.onClick.AddListener( () => ClickButton(false) );
        metalButton.onClick.AddListener( () => ClickButton(true) );
        RandomCard();
    }

    private IEnumerator LaunchActivity(Activity activity){
        yield return StartCoroutine(FadeScreen(true, fadeSpeed));
        if(activity == Activity.LoadGame){
            mainMenu.SetActive(false);
            InitGame();
        }
        else if(activity == Activity.GuitarHero){
            StartCoroutine("GuitarHeroEvent");
            
        }
        
        yield return StartCoroutine(FadeScreen(false, fadeSpeed));
        yield break;
    }

    private IEnumerator FadeScreen(bool fadeToBlack, int fadeSpeed){
        GameObject fadeFrame = GameObject.Find("FadeFrame");
        Color objectColor = fadeFrame.GetComponent<Image>().color;
        float fadeAmount;
        if(fadeToBlack){
            while(fadeFrame.GetComponent<Image>().color.a < 1){
                fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);
                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                fadeFrame.GetComponent<Image>().color = objectColor;
                yield return null;
            }
        }
        else{
            while(fadeFrame.GetComponent<Image>().color.a > 0){
                fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);
                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                fadeFrame.GetComponent<Image>().color = objectColor;
                yield return null;
            }
        }
        yield break;
    }

    private void ClickButton(bool isMetalist){  // answers for this encounter: step + 1 = normis; step + 2 = metalist 
        if(!isMetalist){    // NU OCHEVIDNO ETO TUPO KOSTYLYOK NADA ISPRAVIT
            SpawnCard(CARD_TEXT[step], SPRITES[step], IS_ENCOUNTER[step]);
            step += 2;
        }
        else{
            step ++;
            SpawnCard(CARD_TEXT[step], SPRITES[step], IS_ENCOUNTER[step]);
            step ++;
        }
    }

    public void RandomCard(){
        if(currentCard != null) // Destroing previous card
            Destroy(currentCard);

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
                SpawnCard(CARD_TEXT[step], SPRITES[step], IS_ENCOUNTER[step]);
                if(step == 54)
                    StartCoroutine(LaunchActivity(Activity.GuitarHero));
            }
            else
            {
                end.SetActive(true);
            }
        }
        step++; 
    }

    

    private void SpawnCard(string text, Sprite image, bool isEncounter){ 
        if(currentCard != null) // Destroing previous card
            Destroy(currentCard);

        // instantiating card game object
        GameObject toCreate = isEncounter ? encounterCardPrefab : answerCardPrefab; 
        currentCard = Instantiate(toCreate, new Vector3(0,0,0), Quaternion.identity);

        //setting card's parameters
        text = text.Replace("@", Environment.NewLine); // @ is an IMPORTANT symbol which says the system to ADD NEW LINE.  
        currentCard.GetComponentInChildren<Text>().text = text;
        currentCard.transform.Find("Art").GetComponent<Image>().sprite = image;
        currentCard.transform.SetParent(GameObject.Find("CurrentCardLayer").transform); // making Canvas a parent object of the card to make it visible on the UI
        currentCard.GetComponent<RectTransform>().anchoredPosition = new Vector2(0.5f, 0); // setting position on the center of the Canvas
        // Debug.Log("SpawnCard: " + currentCard);
    }

    public bool setActiveButtons(bool isActive){
        normisButtonObject.SetActive(isActive);
        metalButtonObject.SetActive(isActive);
        Debug.Log("Buttons Activated: " + isActive);
        return isActive;
    }

    public IEnumerator GuitarHeroEvent()
    {   
        yield return SceneManager.LoadSceneAsync("GuitarHero", LoadSceneMode.Additive);
        canvas.SetActive(false);
        GameObject audioManagerObj = GameObject.Find("AudioManager");
        AudioManager audioManager = audioManagerObj.GetComponent<AudioManager>();
        audioManager.GuitarMusicPlayer(0);
        canvas.SetActive(false);
        //AudioManager.SetActive(false);
        //GameObject guitarAudioManager = GameObject.Find("GuitarAudioManager");
        //guitarHeroAudio guitarHeroAudio = guitarAudioManager.GetComponent<GuitarHeroAudio>();
        //guitarHeroAudio.track = 0;
        //guitarHeroAudio.MusicPlayer();
        yield return new WaitForSeconds(15);
        Debug.Log("the end of guitar hero");
        canvas.SetActive(true);
        SceneManager.UnloadSceneAsync("GuitarHero");
        yield break;
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
}
