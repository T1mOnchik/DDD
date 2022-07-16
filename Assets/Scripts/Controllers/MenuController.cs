using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{

    private Image menuLayoutOfInteract;
    private GameObject settingsMenu;
    private bool isMenuActive = false;
    private GameObject mainMenu;
    private GameObject showMenuButtonObj;
    private GameObject creditScreen;

    private Button startButton;
    private Button settingsButton;
    private Button creditsButton;
    private Button exitButton;
    private Button returnButton;
    private Text musicVolumeLabel;
    private Text effectsVolumeLabel;
    private Text languageLabel;

    // Start is called before the first frame update
    void Start()
    {
        //main menu
        mainMenu = GameObject.Find("MainMenu");
        startButton = GameObject.Find("StartButton").GetComponent<Button>();
        settingsButton = GameObject.Find("SettingsButton").GetComponent<Button>();
        creditsButton = GameObject.Find("CreditsButton").GetComponent<Button>();
        exitButton = GameObject.Find("ExitButton").GetComponent<Button>();
        creditScreen = GameObject.Find("Credits");

        //settings
        Button menuLayoutButton = GameObject.Find("MenuLayout").GetComponent<Button>();
        showMenuButtonObj = GameObject.Find("ShowMenuButton");
        Button showMenuButton = showMenuButtonObj.GetComponent<Button>();
        returnButton = GameObject.Find("Return").GetComponent<Button>();
        showMenuButtonObj.SetActive(false);
        
        menuLayoutOfInteract = GameObject.Find("MenuLayout").GetComponent<Image>();
        settingsMenu = GameObject.Find("Settings");

        startButton.onClick.AddListener( () => {
            UIManager.instance.LaunchActivity(UIManager.Activity.LoadGame);
            showMenuButtonObj.SetActive(true);
            });
        settingsButton.onClick.AddListener(ShowHideMenu);
        creditsButton.onClick.AddListener(() => ShowHideCredits(true));
        creditScreen.GetComponent<Button>().onClick.AddListener(() => ShowHideCredits(false));
        exitButton.onClick.AddListener(ExitGame);
        
        showMenuButton.onClick.AddListener(ShowHideMenu);
        menuLayoutButton.onClick.AddListener(() => { if(isMenuActive) ShowHideMenu(); });
        returnButton.onClick.AddListener(ShowHideMenu);

        creditScreen.SetActive(false);
        settingsMenu.SetActive(false);
        menuLayoutOfInteract.raycastTarget = false;
    }

    public void ApplyLanguagePack(CSVParser.Language language){
        Dictionary<string, string> strings = new CSVParser().GetUserInterfaceStrings(language);
        startButton.GetComponentInChildren<Text>().text = strings["StartButton"];
        settingsButton.GetComponentInChildren<Text>().text = strings["SettingsButton"];
        creditsButton.GetComponentInChildren<Text>().text = strings["CreditsButton"];
        exitButton.GetComponentInChildren<Text>().text = strings["ExitButton"];
        returnButton.GetComponentInChildren<Text>().text = strings["ReturnLabel"];
        GameObject.Find("MusicVolumeLabel").GetComponent<Text>().text = strings["MusicLabel"];
        GameObject.Find("EffectsVolumeLabel").GetComponent<Text>().text = strings["EffectsLabel"];
        GameObject.Find("LanguageLabel").GetComponent<Text>().text = strings["LanguageLabel"];
    }

    private void ShowHideMenu(){
        settingsMenu.SetActive(!isMenuActive);
        isMenuActive = !isMenuActive;
        menuLayoutOfInteract.raycastTarget = isMenuActive;
    }

    private void ShowHideCredits(bool isActive){
        creditScreen.SetActive(isActive);
    }

    private void ExitGame(){
        UIManager.instance.LaunchActivity(UIManager.Activity.QuitGame);
    }
    
}
