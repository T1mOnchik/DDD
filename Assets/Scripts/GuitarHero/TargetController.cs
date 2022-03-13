using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{ 
    public GameObject needle;
    public QualitySliderController qualitySliderController;
    [SerializeField]private GameObject currentCircle;
    [SerializeField]private float qualityCounter;
    [SerializeField]private bool active = false;

    void Start()
    {
        qualitySliderController = needle.GetComponent<QualitySliderController>();
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
        if(qualitySliderController.destination.x < 3f)
        qualitySliderController.destination.x += 2;
    }

    void SubtractScore()
    {  
        if(qualitySliderController.destination.x > -3f)
        qualitySliderController.destination.x -= 2;
    } 
    
}
