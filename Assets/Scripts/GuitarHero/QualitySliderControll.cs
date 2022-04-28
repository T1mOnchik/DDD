using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QualitySliderControll : MonoBehaviour
{
    public static QualitySliderControll instance;
    float smoothTime = 0.5f;
    float yVelocity = 0.0f;
    private Slider slider;
    public float destination = 0f;
    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
            instance = this;
        if(instance != this)
            Destroy(instance);
        
        slider = GetComponent<Slider>(); 
    }

    // Update is called once per frame
    void Update()
    {
        MoveNeedle();
    }

    void MoveNeedle()
    {
        float newPosition = Mathf.SmoothDamp(slider.value, destination, ref yVelocity, smoothTime);
        slider.value = newPosition;
    }

    public float GetSliderValue(){
        return slider.value;
    }
}
