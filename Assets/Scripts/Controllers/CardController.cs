using UnityEngine;
using UnityEngine.UI;

public class CardController : MonoBehaviour
{

    // Доделать контроллер со всем юаем, сюда надо скармливать ток карту
    [HideInInspector]public Button cardPanel;
    [HideInInspector]public Card card;
    private Animator animator;

    private void Start() {
        animator = transform.Find("Card").GetComponent<Animator>();
        
    }

    public void SetCardInfo(Card card){
        this.card = card;
        InitializeCardUI();
        if(!card.isEncounter){
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

    private void InitializeCardUI(){
        this.transform.SetParent(GameObject.Find("SpawnPoint").transform); // setting Canvas as a parent object of the card to make it visible on the UI
        GetComponentInChildren<Text>().text = card.text;
        transform.Find("Card").transform.Find("Art").GetComponent<Image>().sprite = card.sprite;
        GetComponent<RectTransform>().anchoredPosition = new Vector2(0.5f, 0); // setting position on the center of the Canvas
    }

}
