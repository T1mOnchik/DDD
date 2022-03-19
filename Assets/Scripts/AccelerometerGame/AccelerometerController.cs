using System.Collections;
using UnityEngine;

public class AccelerometerController : MonoBehaviour
{
    [SerializeField]private Vector3 acceleration;
    public float speed;
    public bool result;
    private float random;
    private Vector3 sliderSize;
    private float needleBorder;
    private GameObject background;
    // Start is called before the first frame update
    void Start()
    {
        background = GameObject.Find("Background");
        StartCoroutine("DelayRandom");
        // sliderSize = transform.parent.GetComponent<Renderer>().bounds.size;
        // needleBorder = sliderSize.x / 2 - 0.5f;
        // Debug.Log(needleBorder);
        StartCoroutine("DelayCoroutine");
    }

    // Update is called once per frame
    void Update()
    {   
        //if(transform.localPosition.x < needleBorder && transform.localPosition.x > -needleBorder)
        if(transform.localPosition.x < 3.5 && transform.localPosition.x > -3.5)
        {
            acceleration = Input.acceleration;
            GetComponent<Rigidbody>().velocity = new Vector3(acceleration.x * speed + random, 0f, 0f);
        }
        //else if(transform.localPosition.x > needleBorder)
        else if(transform.localPosition.x > 3.5)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(-1f, 0f, 0f);
        }
        //else if(transform.localPosition.x < -needleBorder)
        else if(transform.localPosition.x < -3.5)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(1f, 0f, 0f);
        }
    }

    private IEnumerator DelayRandom()
    {
        while(true)
        {
            //random = Random.Range(-sliderSize.x / 4, sliderSize.x / 4);
            random = Random.Range(-2, 2);
            yield return new WaitForSeconds(1f);
            yield return null;
        }
    }

    private IEnumerator DelayCoroutine()
    {
        background.SetActive(false);
        yield return new WaitForSeconds(10f);
        if(transform.localPosition.x > -0.8 && transform.localPosition.x < 0.8)
        {
            result = true;
        }
        else
        {
            result = false;
        }
        background.SetActive(true);
        GameManager.instance.OnMinigameFinished(result);
        Debug.Log(result);
        //выход из игры
        yield break;
    }
}
