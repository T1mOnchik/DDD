using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GuitarHeroManager : MonoBehaviour
{
    public static GuitarHeroManager instance;
    private GameObject canvas;
    public bool result;
    private Coroutine eventInstance;

    [SerializeField]private float time = 59f; // time after which guitarhero will be closed
    [SerializeField]private float winScore = 95f;
    public float pointPerTap; 
    public int missMultiplicator;
    
    // Start is called before the first frame update
    void Start()
    {
        if(instance != this)
            Destroy(instance);
        if(instance == null)
            instance = this;


        canvas = GameObject.Find("Background");
        if(eventInstance != null)
            StopCoroutine(eventInstance);
        eventInstance = StartCoroutine(GuitarHeroEvent());
    }

    public IEnumerator GuitarHeroEvent()
    {   
        canvas.SetActive(false);
        AudioManager.instance.LaunchGuitarMusicPlayer(time);
        yield return new WaitForSeconds(time);
        CheckingResults();
        canvas.SetActive(true);
        // Debug.Log("GuitarHero: " + result);
        GameManager.instance.OnMinigameFinished(result);
        SceneManager.UnloadSceneAsync("GuitarHero");
        yield break;
    }

    private void CheckingResults()
    {
       if(QualitySliderControll.instance.GetSliderValue() >= winScore)
            result = true;
       else
            result = false;
    }
}
