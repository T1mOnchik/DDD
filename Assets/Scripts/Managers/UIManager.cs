using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance = null;
    private GameObject mainMenu;
    private GameObject canvas;
    [SerializeField]private GameObject slider;
    
    [Header("Visual Effects")]    
    [SerializeField]private int fadeSpeed = 2;
    public enum Activity{
        LoadGame,
        GuitarHero,
        AccelerometerGame,
        SliderGame
    }

    // Start is called before the first frame update
    void Start()
    {
        if(instance != this)
            Destroy(instance);
        if(instance == null)
            instance = this;

        // canvas = GameObject.Find("BottomPanel");
        mainMenu = GameObject.Find("MainMenu");
        mainMenu.GetComponent<Button>().onClick.AddListener( () => LaunchActivity(Activity.LoadGame));
    }

    public void LaunchActivity(Activity activity){
        StartCoroutine(LaunchActivityCoroutine(activity));
    }

    private IEnumerator LaunchActivityCoroutine(Activity activity){
        yield return StartCoroutine(FadeScreen(true, fadeSpeed));
        if(activity == Activity.LoadGame){
            mainMenu.SetActive(false);
            GameManager.instance.InitGame();
        }
        else if(activity == Activity.GuitarHero){
            yield return SceneManager.LoadSceneAsync("GuitarHero", LoadSceneMode.Additive);
            GameManager.instance.RandomCard();
        }
        else if(activity == Activity.SliderGame){
            yield return StartCoroutine(FadeScreen(false, fadeSpeed));
            yield return GameManager.instance.SliderGame();
            GameManager.instance.RandomCard();
        }
        else if(activity == Activity.AccelerometerGame){
            yield return StartCoroutine(FadeScreen(false, fadeSpeed));
            // GameManager.instance.RandomCard();
            yield return SceneManager.LoadSceneAsync("HaterFight", LoadSceneMode.Additive);
            GameManager.instance.RandomCard();
            
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

}
