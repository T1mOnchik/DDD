using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QualitySliderController : MonoBehaviour
{
    public Vector2 destination = new Vector2(0, 0);
    private Vector2 velocity;

    void Update()
    {
        MoveNeedle();
    }

    public void MoveNeedle()
    { 
        transform.localPosition = Vector2.SmoothDamp(transform.localPosition, destination, ref velocity, 1, 5, Time.deltaTime);
    }
   
}
