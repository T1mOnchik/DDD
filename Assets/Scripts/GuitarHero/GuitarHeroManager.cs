using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GuitarHeroManager : MonoBehaviour
{
    public static GuitarHeroManager instance;
    private GameObject canvas;
    [SerializeField]private float time = 58f; // time after which guitarhero will be closed
    private Coroutine eventInstance;
    public int guitarHeroScore = 0;

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
        canvas.SetActive(true);
        SceneManager.UnloadSceneAsync("GuitarHero");
        yield break;
    }
}
