using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   
    public static GameManager instance = null;
    
    // Database simulatation
    [SerializeField]private List<string> CARD_TEXT;
    [SerializeField]private List<Sprite> SPRITES;
    [SerializeField]private List<bool> IS_ENCOUNTER;

    // UI references
    [SerializeField]private GameObject canvas;
    [SerializeField]private GameObject angelButtonObject;
    [SerializeField]private GameObject demonButtonObject;
    //    [SerializeField]private GameObject sliderObj;
    [SerializeField]private GameObject end;
    [SerializeField]private GameObject encounterCardPrefab;
    [SerializeField]private GameObject answerCardPrefab;
    private Button angelButton;
    private Button demonButton;
    private ProgressBarController moneyProgressBar;
    private ProgressBarController psycheProgressBar;
    private ProgressBarController popularityProgressBar;

    public bool sliderCheck = false;
    public int guitarHeroScore = 0;
    
    private GameObject currentCard;
    private int step = 0;
    
    
    // Start is called before the first frame update
    void Start()
    {   
        if(instance != this)
            Destroy(instance);
        if(instance == null)
            instance = this;

        angelButtonObject = GameObject.Find("AngelButton");
        angelButton = angelButtonObject.GetComponent<Button>();
        demonButton = GameObject.Find("DemonButton").GetComponent<Button>();
        moneyProgressBar = GameObject.Find("Money").GetComponent<ProgressBarController>();
        psycheProgressBar = GameObject.Find("Psyche").GetComponent<ProgressBarController>();
        popularityProgressBar = GameObject.Find("Popularity").GetComponent<ProgressBarController>();

        // countOfAllEncounters = startEncounters.Count + concertEncounters.Count + concertEncounters.Count + randomEncounters.Count;

        angelButton.onClick.AddListener( () => ClickButton(true) );
        demonButton.onClick.AddListener( () => ClickButton(false) );
        RandomCard();
        //StartCoroutine("SliderEvant");
        // StartCoroutine("GuitarHeroEvent");
    }

    private void ClickButton(bool isAngel){
        moneyProgressBar.current += currentCard.GetComponent<EventCardModel>().demonDecision.moneyImpact;
        psycheProgressBar.current += currentCard.GetComponent<EventCardModel>().demonDecision.psycheImpact;
        popularityProgressBar.current += currentCard.GetComponent<EventCardModel>().demonDecision.popularityImpact;
        RandomCard();
        // if(isAngel)
        //     SpawnCard(currentCard.GetComponent<EventCardModel>().cardAngelAnswer);
        // else
        //     SpawnCard(currentCard.GetComponent<EventCardModel>().cardDemonAnswer);
    }

    public void RandomCard(){
        if(currentCard != null) // Destroing previous card
            Destroy(currentCard);

        Debug.Log("step: "+step);
        if(moneyProgressBar.current <= 0){
            // Debug.Log("Sdoh iz za malo deneg");
            end.SetActive(true);
        }
        else if(moneyProgressBar.current >= 100){
            // Debug.Log("Sdoh iz za mnogo deneg");
            end.SetActive(true);
        }
        else if(psycheProgressBar.current <= 0){
            // Debug.Log("Sdoh iz za malo stressa");
            end.SetActive(true);
        }
        else if(psycheProgressBar.current >= 100){
            // Debug.Log("Sdoh iz za mnogo stressa");
            end.SetActive(true);
        }
        else if(popularityProgressBar.current <= 0){
            // Debug.Log("Sdoh iz za malo popularnosti");
            end.SetActive(true);
        }
        else if(popularityProgressBar.current >= 100){
            // Debug.Log("Sdoh iz za mnogo popularnosti");
            end.SetActive(true);
        }
        else{
            if(step < CARD_TEXT.Count){
                Debug.Log("step < CARD_TEXT.Count");
                SpawnCard(CARD_TEXT[step], SPRITES[step], IS_ENCOUNTER[step]);
            }
            else
            {
                end.SetActive(true);
            }
            
        
        }
        step++; 
    }

    private void SpawnCard(string text, Sprite image, bool isEncounter){ 

        // instantiating card game object
        GameObject toCreate = isEncounter ? encounterCardPrefab : answerCardPrefab; 
        currentCard = Instantiate(toCreate, new Vector3(0,0,0), Quaternion.identity);

        //setting card's parameters
        text = text.Replace("@", Environment.NewLine); // @ is an IMPORTANT symbol which says the system to ADD NEW LINE.  
        currentCard.GetComponentInChildren<Text>().text = text;
        currentCard.transform.Find("Art").GetComponent<Image>().sprite = image;
        currentCard.transform.SetParent(GameObject.Find("Canvas").transform); // making Canvas a parent object of the card to make it visible on the UI
        currentCard.GetComponent<RectTransform>().anchoredPosition = new Vector2(0.5f, 0); // setting position on the center of the Canvas
        Debug.Log("SpawnCard: " + currentCard);
    }

    public bool SetStopButtonsBool(bool isStop){
        angelButtonObject.SetActive(isStop);
        demonButtonObject.SetActive(isStop);
        Debug.Log("Buttons Activated: " + isStop);
        return isStop;
    }

    public IEnumerator GuitarHeroEvent()
    {   
        // while()
        canvas.SetActive(false);
        SceneManager.LoadSceneAsync("GuitarHero", LoadSceneMode.Additive);
        GameObject audioManagerObj = GameObject.Find("AudioManager");
        AudioManager audioManager = audioManagerObj.GetComponent<AudioManager>();
        audioManager.GuitarMusicPlayer(0);
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
