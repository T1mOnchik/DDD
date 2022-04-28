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
    [SerializeField]private float points = 4f;
    [SerializeField]private int missMultiplicator = 3;

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
            qualitySliderControll.destination += points;
    }

    void SubtractScore()
    {   
        if(qualitySliderControll.destination > 0)
            qualitySliderControll.destination -= points * missMultiplicator;
    } 
    
}
