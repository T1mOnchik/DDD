using UnityEngine;
using UnityEngine.UI;

public class AnswerCard : MonoBehaviour
{

    public Button cardPanel;

    private void Start() {
        GameManager.instance.SetStopButtonsBool(false);
        cardPanel = GetComponent<Button>();
        cardPanel.onClick.AddListener(DestroyThis);
        // Invoke("DestroyThis", 3f);
    }


    // Update is called once per frame
    void Update()
    {

    }

    private void DestroyThis(){
        GameManager.instance.SetStopButtonsBool(true);    
        GameManager.instance.RandomCard();
        Destroy(gameObject);
    }
    
}
