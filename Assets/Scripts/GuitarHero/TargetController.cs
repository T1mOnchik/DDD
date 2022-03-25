using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{ 
    public GameObject slider;
    public QualitySliderControll qualitySliderControll;
    [SerializeField]private GameObject currentCircle;
    [SerializeField]private float qualityCounter;
    [SerializeField]private bool active = false;

    void Start()
    {
        qualitySliderControll = slider.GetComponent<QualitySliderControll>();
    }
 
    void OnTriggerEnter2D(Collider2D other)
    {
        currentCircle = other.gameObject;
        active = true;
    }

    void OnTriggerExit2D()
    {   
        if(active)
        {
            SubtractScore();
            active = false;
        }
    }

    void OnMouseDown()
    {
        if(active)
        {   
            active = false;
            Destroy(currentCircle);
            AddScore();
        }
    }

    void AddScore()
    {  
        if(qualitySliderControll.destination < 100)
        qualitySliderControll.destination += 11f;
    }

    void SubtractScore()
    {   
        if(qualitySliderControll.destination > 0)
        qualitySliderControll.destination -= 15f;
    } 
    
}
