using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

[Serializable]
public class Card : MonoBehaviour
{

    public Button cardPanel;
    [HideInInspector]public string text;
    [HideInInspector]public string spriteName;
    [HideInInspector]public int moneyImpact;
    [HideInInspector]public int psycheImpact;
    [HideInInspector]public int popularityImpact;
    [HideInInspector]public bool isEncounter;
    private Animator animator;
    
    public Card(string rowData){
        string [] data = rowData.Split(';');
        
        try{
            this.text = data[0];
            this.spriteName = data[1];
            this.moneyImpact = int.Parse(data[2]);
            this.psycheImpact = int.Parse(data[3]);
            this.popularityImpact = int.Parse(data[4]);
            this.isEncounter = bool.Parse(data[5]);
        }
        catch(Exception e){
            Debug.LogException(e);
        }
        
    }

    private void Start() {
        animator = transform.Find("Card").GetComponent<Animator>();
        if(!isEncounter){
            cardPanel = GameObject.Find("Background").GetComponent<Button>();            
            Invoke("ActivateCard", 0.5f);
        }
    }

    private void ActivateCard(){
        cardPanel.onClick.AddListener(DestroyThis);
    }

    private void DestroyThis(){
        cardPanel.onClick.RemoveAllListeners();
        GameManager.instance.NextCard("MoveToMetal");
    }

    public float RemoveCardAnimation(string animName){
        if(cardPanel != null)
            cardPanel.onClick.RemoveAllListeners();
        animator.SetTrigger(animName);
        return animator.runtimeAnimatorController.animationClips[0].length;
    }

}
