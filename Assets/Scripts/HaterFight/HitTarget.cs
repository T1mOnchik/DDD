using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitTarget : MonoBehaviour
{   
    private GameObject hater;
    private HaterController haterController;
    // Start is called before the first frame update
    void Start()
    {
        hater = GameObject.Find("Hater");
        haterController = hater.GetComponent<HaterController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseDown()
    {
       haterController.Hit();
    }
    

}
