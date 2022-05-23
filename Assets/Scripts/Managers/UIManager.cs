using System.Collections;
using System;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance = null;
    private GameObject mainMenu;
    private GameObject menu;
    private GameObject canvas;
    private GameObject moneyIndicatorImage;
    private GameObject stressIndicatorImage;
    private GameObject popIndicatorImage;
    private Scene currentScene;
    [SerializeField]private GameObject slider;
    [SerializeField]private GameObject indicatorDescription;
    
    [Header("Visual Effects")]    
    [SerializeField]private int fadeSpeed = 2;
    public enum Activity{
        LoadGame,
        QuitGame,
        GuitarHero,
        JumpGame,
        HaterFight,
        Lose
    }

    // Start is called before the first frame update
    void Start()
    {
        if(instance != this)
            Destroy(instance);
        if(instance == null)
            instance = this;

        currentScene = SceneManager.GetActiveScene();
        canvas = GameObject.Find("Background");
        mainMenu = GameObject.Find("MainMenu");
        menu = GameObject.Find("MenuLayout");
        indicatorDescription = GameObject.Find("IndicatorDescription");
        moneyIndicatorImage = GameObject.Find("MoneyImage");
        stressIndicatorImage = GameObject.Find("StressImage");
        popIndicatorImage = GameObject.Find("PopImage");
        indicatorDescription.SetActive(false);
        moneyIndicatorImage.GetComponent<Button>().onClick.AddListener(delegate{OnIndicatorClicked(0);});
        stressIndicatorImage.GetComponent<Button>().onClick.AddListener(delegate{OnIndicatorClicked(1);});
        popIndicatorImage.GetComponent<Button>().onClick.AddListener(delegate{OnIndicatorClicked(2);});
        indicatorDescription.transform.Find("IndicatorDescriptionPanel").GetComponent<Button>().onClick.AddListener( () => indicatorDescription.SetActive(false) );
    }

    private void OnIndicatorClicked(int indicatorNum){
        Sprite sprite;
        string text;
        switch(indicatorNum){

            case 0:
                sprite = moneyIndicatorImage.GetComponent<Image>().sprite;
                text = "Это показатель денег.@Овациями сыт не будешь, поэтому группа всегда должна быть при деньгах,@поэтому следи, чтоб денег было не слишком мало, но и не слишком много";
                break;

            case 1:
                sprite = stressIndicatorImage.GetComponent<Image>().sprite;
                text = "Это показатель стресса@Стресс всегда преследует звёзд. Старайся не съехать с катушек.@Много стресса точно плохо скажется на твоем состоянии, но и чересчур расслабленным быть тоже вредно";
                break;
            case 2:
                sprite = popIndicatorImage.GetComponent<Image>().sprite;
                text = "Это показатель популярности@Без зрителей вы никому не нужны, поэтому поддерживай уровень популярности. Если он будет слишком низкий никто не будет ходить на ваши концерты и покупать мерч, а если станете слишком популярными...@ты же слышал про Джона Ленона?";
                break;

            default:
                sprite = null;
                text = "Честно, я без понятия, что ты нажал, что это появилось, лучше напиши об этом разрабам!";
                break;
        }
        indicatorDescription.SetActive(true);
        text = text.Replace("@", Environment.NewLine);
        indicatorDescription.transform.Find("Image").GetComponent<Image>().sprite = sprite;
        indicatorDescription.transform.Find("Text").GetComponent<Text>().text = text;
    }

    public void LaunchActivity(Activity activity){
        StartCoroutine(LaunchActivityCoroutine(activity));
    }

    public void ApplyLanguagePack(CSVParser.Language language){
        menu.GetComponent<MenuController>().ApplyLanguagePack(language);
    }

    private IEnumerator LaunchActivityCoroutine(Activity activity){
        yield return StartCoroutine(FadeScreen(true, fadeSpeed));
        if(activity == Activity.LoadGame){
            mainMenu.SetActive(false);
            GameManager.instance.InitGame();
        }
        else if(activity == Activity.Lose)
            SceneManager.LoadScene("MainScene");

        else if(activity == Activity.GuitarHero){
            yield return SceneManager.LoadSceneAsync("GuitarHero", LoadSceneMode.Additive);
        }
        else if(activity == Activity.HaterFight){
            yield return SceneManager.LoadSceneAsync("HaterFight", LoadSceneMode.Additive);
        }
        else if(activity == Activity.JumpGame){
            yield return SceneManager.LoadSceneAsync("JumpGame", LoadSceneMode.Additive);
        }
        else if(activity == Activity.QuitGame)
            Application.Quit();
        
        yield return StartCoroutine(FadeScreen(false, fadeSpeed));
        yield break;
    }

    private IEnumerator FadeScreen(bool fadeToBlack, int fadeSpeed){
        GameObject fadeFrame = GameObject.Find("FadeFrame");
        Image fadeFrameImage = fadeFrame.GetComponent<Image>(); // We make fadeFrameImage raycast target true 
        fadeFrameImage.raycastTarget = true;                    // to block clicks on other objects until screen fade won't be finished 
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
        fadeFrameImage.raycastTarget = false;                      //Here we turning on clicking on other objects
        yield break;
    }

}
