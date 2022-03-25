using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QualitySliderControll : MonoBehaviour
{
    float smoothTime = 1f;
    float yVelocity = 0.0f;
    private Slider slider;
    public float destination = 0f;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>(); 
        StartCoroutine("DelayCoroutine");
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

    private IEnumerator DelayCoroutine()
    {
       yield return new WaitForSeconds(58f);
       if(slider.value >= 75f)
       {
            GuitarHeroManager.instance.result = true;
       }
       else
       {
           GuitarHeroManager.instance.result = false;
       }
       yield break;
    }
}
