using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HaterGameManager : MonoBehaviour
{
    [SerializeField]private List<GameObject> targets;
    [SerializeField]private List<Sprite> sprites;
    private int i = 0;
    private int winCounter = 0;
    private int deathCounter = 0;
    private GameObject canvas;
    private int counter = 1;
    private Animator animator;
    private Vector2 velocity;
    private HaterObjController haterObjController;
    private SpriteRenderer spriteR;

    public bool result;
    public Vector2 destination = new Vector2(4, 0);
    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Background");    
        spriteR = gameObject.GetComponent<SpriteRenderer>();
        haterObjController = transform.parent.gameObject.GetComponent<HaterObjController>();
        animator = GetComponent<Animator>();
        TargetActivator();
    }

    private IEnumerator DelayCoroutine()
    {
        canvas.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        targets[i-1].SetActive(false);
        TargetActivator();
        if(counter == 1)
        {
            haterObjController.destination = new Vector2(4, 0);
                    counter++;
        }
        else if(counter == 2)
        {
            haterObjController.destination = new Vector2(-4, -1.2f);
            counter++;
        }
        else if(counter == 3)
        {
            haterObjController.destination = new Vector2(4, -1.2f);
            counter++;
        }
        else if(counter == 4)
        {
            haterObjController.destination = new Vector2(-4, 0);
            counter = 1;
        }
        deathCounter++;
        if(deathCounter == 3)
        {
            StopCoroutine("DelayCoroutine");
            result = false;
            //smert
            Debug.Log("Lose");
            this.gameObject.SetActive(false);
            canvas.SetActive(true);
            GameManager.instance.OnMinigameFinished(result);
            SceneManager.UnloadSceneAsync("HaterFight");
        }
    }
    private void TargetActivator()
    {   
        if(i < 5)
        {
            targets[i].SetActive(true);
            i++;
        }
        else
        {
            i = 0;
            targets[i].SetActive(true);
            i++;
        }
        StartCoroutine("DelayCoroutine");
    }
    public void Hit()
    {
        animator.SetTrigger("RightTrigger");
        winCounter++;
        Debug.Log(targets[i-1]);
        targets[i-1].SetActive(false);
        StopCoroutine("DelayCoroutine");
        if(winCounter == 12)
        {
            result = true;
            //pabeda
            Debug.Log("Win");
            this.gameObject.SetActive(false);
            canvas.SetActive(true);
            GameManager.instance.OnMinigameFinished(result);
            SceneManager.UnloadSceneAsync("HaterFight");
        }
        else
        {
            if(winCounter % 2 == 0)
            {
                spriteR.sprite = sprites[winCounter / 2 - 1];
                Debug.Log(sprites[winCounter / 2 - 1]);
            }
            TargetActivator();
            if(counter == 1)
            {
                haterObjController.destination = new Vector2(4, 0);
                counter++;
            }
            else if(counter == 2)
            {
                haterObjController.destination = new Vector2(-4, -1.2f);
                counter++;
            }
            else if(counter == 3)
            {
                haterObjController.destination = new Vector2(4, -1.2f);
                counter++;
            }
            else if(counter == 4)
            {
                haterObjController.destination = new Vector2(-4, 0);
                counter = 1;
            }

        }
    }
}
