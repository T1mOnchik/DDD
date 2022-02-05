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
    // Start is called before the first frame update
    void Start()
    {
       gameManager = GameObject.Find("GameManager");
       GameManager = gameManager.GetComponent<GameManager>();
       slider = GetComponent<Slider>(); 
       StartCoroutine("MoveSlider");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseDown()
    {
        mouseDown = true;
        //Debug.Log("111");
    }
    private IEnumerator MoveSlider()
    {
        while(mouseDown == false)
        { 
            if(moveToRight)
            {
                slider.value += 0.025f;
                yield return new WaitForSeconds(0.0025f);
                if(slider.value == 1)
                {
                    moveToRight = false;
                }
            }
            else
            {
                slider.value -= 0.025f;
                yield return new WaitForSeconds(0.0025f);
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
            //положительное событие
        }
        else
        {
            //отрицательное событие
        }
        //Debug.Log("Nice");
        yield break;
    }
}
