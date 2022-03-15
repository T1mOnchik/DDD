using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

[Serializable]
public class Card : MonoBehaviour
{

    public Button cardPanel;
    [HideInInspector]public string encounterText;
    [HideInInspector]public string normisText;
    [HideInInspector]public string metalText;
    // public Sprite sprite;
    [HideInInspector]public string eSpriteName;
    [HideInInspector]public string nSpriteName;
    [HideInInspector]public string mSpriteName;
    [HideInInspector]public float moneyImpactNormis;
    [HideInInspector]public float moneyImpactMetal;
    [HideInInspector]public float psycheImpactNormis;
    [HideInInspector]public float psycheImpactMetal;
    [HideInInspector]public float popularityImpactNormis;
    [HideInInspector]public float popularityImpactMetal;
    [HideInInspector]public bool isEncounter;
    private Animator animator;
    
    public Card(string rowData){
        string [] data = rowData.Split(';');
        
        this.encounterText = data[0]; 
        this.eSpriteName = data[1];
        this.normisText = data[2];
        this.nSpriteName = data[3];
        this.metalText = data[4];
        this.mSpriteName = data[5];
        this.moneyImpactNormis = float.Parse(data[6]);
        this.psycheImpactNormis = float.Parse(data[7]);
        this.popularityImpactNormis = float.Parse(data[8]);
        this.moneyImpactMetal = float.Parse(data[9]);
        this.psycheImpactMetal = float.Parse(data[10]);
        this.popularityImpactMetal = float.Parse(data[11]);
    }

    private void OnEnable() {
        animator = transform.Find("Card").GetComponent<Animator>();
        if(!isEncounter){
            cardPanel = GameObject.Find("Background").GetComponent<Button>();            
            Debug.Log(GameObject.Find("Background").GetComponent<Button>());
            Invoke("ActivateCard", 0.7f);
            Debug.Log("Start otrabotal: " + cardPanel);
        }
    }

    private void ActivateCard(){
        // Debug.Log(cardPanel);
        cardPanel.onClick.AddListener(DestroyThis);
    }

    private void DestroyThis(){
        // Debug.Log("destr "+cardPanel.onClick);
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
