using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public GameObject gameManager;
    public GameManager GameManager;
    public bool mouseDown = false;
    public Slider slider;
    private bool moveToRight = true;
    public bool result;
    public bool isGameEnd = false;

    private GameObject currentCardLayer;
    private GameObject bottomPanel;
    private GameObject escButton;
    private GameObject normisButton;
    private GameObject metalButton;
    // Start is called before the first frame update
    void Start()
    {
       currentCardLayer = GameObject.Find("CurrentCardLayer");
       bottomPanel = GameObject.Find("BottomPanel");
       escButton = GameObject.Find("EscButton");
       normisButton = GameObject.Find("NormisButton");
       metalButton = GameObject.Find("MetalButton");
       currentCardLayer.SetActive(false);
       bottomPanel.SetActive(false);
        normisButton?.SetActive(false);
       metalButton?.SetActive(false);
       gameManager = GameObject.Find("GameManager");
       GameManager = GameManager.instance;
       slider = GetComponent<Slider>(); 
       StartCoroutine("MoveSlider");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)
        {
            mouseDown = true;
        }
    }

    public IEnumerator MoveSlider()
    {
        while(mouseDown == false)
        { 
            if(moveToRight)
            {
                slider.value += 0.025f;
                yield return new WaitForSeconds(0.025f);
                if(slider.value == 1)
                {
                    moveToRight = false;
                }
            }
            else
            {
                slider.value -= 0.025f;
                yield return new WaitForSeconds(0.025f);
                if(slider.value == 0)
                {
                    moveToRight = true;
                }
            }
            yield return null;
        }
        if(slider.value >= 0.4f && slider.value <= 0.6f)
        {
            GameManager.sliderCheck = true;
            result = true;
            //положительное событие
        }
        else
        {
            result = false;
            //отрицательное событие
        }
        currentCardLayer.SetActive(true);
        bottomPanel.SetActive(true);
        escButton.SetActive(true);
        normisButton.SetActive(true);
        metalButton.SetActive(true);
        isGameEnd = true;
        yield break;
    }
}
