using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{

    private Button settingsButton;
    private Image menuLayoutOfInteract;
    private GameObject menu;
    private GameObject settingsMenu;
    private bool isMenuActive = false;

    // Start is called before the first frame update
    void Start()
    {
        Button menuLayoutButton = GameObject.Find("MenuLayout").GetComponent<Button>();
        Button showMenuButton = GameObject.Find("ShowMenuButton").GetComponent<Button>();
        Button resumeButton = GameObject.Find("ResumeButton").GetComponent<Button>();
        Button exitButton = GameObject.Find("ExitButton").GetComponent<Button>();
        menuLayoutOfInteract = GameObject.Find("MenuLayout").GetComponent<Image>();
        settingsButton = GameObject.Find("SettingsButton").GetComponent<Button>();
        menu = GameObject.Find("Menu");
        settingsMenu = GameObject.Find("Settings");

        showMenuButton.onClick.AddListener(ShowHideMenu);
        menuLayoutButton.onClick.AddListener(() => { if(isMenuActive) ShowHideMenu(); });
        resumeButton.onClick.AddListener(ShowHideMenu);
        exitButton.onClick.AddListener(ExitGame);

        menu.SetActive(false);
        settingsMenu.SetActive(false);
        menuLayoutOfInteract.raycastTarget = false;
    }

    private void ShowHideMenu(){
        menu.SetActive(!isMenuActive);
        settingsMenu.SetActive(false);
        isMenuActive = !isMenuActive;
        menuLayoutOfInteract.raycastTarget = isMenuActive;
    }

    private void ExitGame(){
        UIManager.instance.LaunchActivity(UIManager.Activity.QuitGame);
    }
    
}
