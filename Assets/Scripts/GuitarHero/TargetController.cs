using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{ 
    private GameObject currentCircle;

    void OnTriggerEnter2D(Collider2D other)
    {
        currentCircle = other.gameObject;
    }
    void OnTriggerExit2D()
    {
        currentCircle = null;
    }
    void OnMouseDown()
    {
        Destroy(currentCircle);
    }
    
}
