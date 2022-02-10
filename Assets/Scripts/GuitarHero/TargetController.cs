using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{   
    public GameObject gameManager;
    public GameManager GameManager;
    private bool click = false; 
    //public List <GameObject> redCircles;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        GameManager = gameManager.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(click);
    }
    void OnTriggerStay2D(Collider2D other)
    {
        //Debug.Log(other);
        DestroyCircle(other.gameObject);
    }
    void OnMouseDown()
    {
        click = true;
        StartCoroutine("ClickDeley");
    }
    private IEnumerator ClickDeley()
    {
        yield return null;
        click = false;
        yield break;
    }
    private void DestroyCircle(GameObject other)
    {
        if(click == true)
        {
            Destroy(other);
            GuitarHeroManager.instance.guitarHeroScore++;
        }
    }
    
}
