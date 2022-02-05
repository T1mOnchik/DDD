using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class ProgressBarController : MonoBehaviour
{

    [SerializeField]private int maximum;
    [SerializeField]public int current;
    [SerializeField]private Image mask;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetCurrentFill();
    }

    private void GetCurrentFill(){
        float fillAmount = (float)current / (float)maximum;
        mask.fillAmount = fillAmount;
    }
}
