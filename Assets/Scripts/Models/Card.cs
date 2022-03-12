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

    private void Start() {
        animator = transform.Find("Card").GetComponent<Animator>();
        if(!isEncounter){
            // GameManager.instance.setActiveButtons(false);
            // StartCoroutine(DeactivateButtons());
            cardPanel = GameObject.Find("CurrentCardLayer").GetComponent<Button>();
            cardPanel.onClick.AddListener(DestroyThis);
            Invoke("ActivateCard", 0.5f);
        }
    }

    private void ActivateCard(){
        cardPanel.enabled = true;
    }

    // private IEnumerator DeactivateButtons(){
    //     yield return new WaitForSeconds(0.1f);
    //     AnimatorStateInfo normisAnim = GameManager.instance.normisButtonAnimator.GetCurrentAnimatorStateInfo(0);
    //     AnimatorStateInfo metalAnim = GameManager.instance.metalButtonAnimator.GetCurrentAnimatorStateInfo(0);
    //     Debug.Log(normisAnim.IsName("Default"));
    //     Debug.Log(metalAnim.IsName("Click"));
    //     if(normisAnim.IsName("NormisClick"))
    //         yield return new WaitForSeconds(normisAnim.length);
    //     if(metalAnim.IsName("MetalClick"))
    //         yield return new WaitForSeconds(metalAnim.length);
    //     GameManager.instance.setActiveButtons(false);
    // }

    private void DestroyThis(){
        
        // GameManager.instance.setActiveButtons(true);    
        GameManager.instance.NextCard("MoveToMetal");
        cardPanel.enabled = false;
        // Destroy(gameObject);
    }

    public float RemoveCardAnimation(string animName){
        animator.SetTrigger(animName);
        return animator.runtimeAnimatorController.animationClips[0].length;
        // mAnimation.Play("MoveToMetal");
        // Debug.Log(animator.);
    }

}
