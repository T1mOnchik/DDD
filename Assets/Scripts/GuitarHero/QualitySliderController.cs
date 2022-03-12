using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QualitySliderController : MonoBehaviour
{
    public Vector2 destination = new Vector2(0, 0);
    private Vector2 velocity;
    public bool result;

    void Start()
    {
        StartCoroutine("DelayCoroutine");
    }
    void Update()
    {
        MoveNeedle();
    }

    public void MoveNeedle()
    { 
        transform.localPosition = Vector2.SmoothDamp(transform.localPosition, destination, ref velocity, 1, 5, Time.deltaTime);
    }
   private IEnumerator DelayCoroutine()
   {
       yield return new WaitForSeconds(58f);
       if(transform.localPosition.x >= 1.4)
       {
            result = true;
       }
       else
       {
           result = false;
       }
       Debug.Log(result);
       yield break;
   }
}
