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

    // [Header("Settings")]
     

    // Start is called before the first frame update
    void Start()
    {
        //main menu
        mainMenu = GameObject.Find("MainMenu");
        Button startButton = GameObject.Find("StartButton").GetComponent<Button>();
        Button settingsButton = GameObject.Find("SettingsButton").GetComponent<Button>();
        Button creditsButton = GameObject.Find("CreditsButton").GetComponent<Button>();
        Button exitButton = GameObject.Find("ExitButton").GetComponent<Button>();

        //settings
        Button menuLayoutButton = GameObject.Find("MenuLayout").GetComponent<Button>();
        showMenuButtonObj = GameObject.Find("ShowMenuButton");
        Button showMenuButton = showMenuButtonObj.GetComponent<Button>();
        Button returnButton = GameObject.Find("Return").GetComponent<Button>();
        showMenuButtonObj.SetActive(false);
        
        menuLayoutOfInteract = GameObject.Find("MenuLayout").GetComponent<Image>();
        settingsMenu = GameObject.Find("Settings");

        startButton.GetComponent<Button>().onClick.AddListener( () => {
            UIManager.instance.LaunchActivity(UIManager.Activity.LoadGame);
            showMenuButtonObj.SetActive(true);
            });
        settingsButton.GetComponent<Button>().onClick.AddListener(ShowHideMenu);
        // creditsButton.
        exitButton.GetComponent<Button>().onClick.AddListener(ExitGame);

        showMenuButton.onClick.AddListener(ShowHideMenu);
        menuLayoutButton.onClick.AddListener(() => { if(isMenuActive) ShowHideMenu(); });
        returnButton.onClick.AddListener(ShowHideMenu);
        settingsMenu.SetActive(false);
        menuLayoutOfInteract.raycastTarget = false;
    }

    private void ShowHideMenu(){
        settingsMenu.SetActive(!isMenuActive);
        isMenuActive = !isMenuActive;
        menuLayoutOfInteract.raycastTarget = isMenuActive;
    }

    private void ExitGame(){
        UIManager.instance.LaunchActivity(UIManager.Activity.QuitGame);
    }
    
}
