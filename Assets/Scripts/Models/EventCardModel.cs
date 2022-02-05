using UnityEngine;
using UnityEngine.UI;
using System;

public class EventCardModel : MonoBehaviour
{

    public DemonDecision demonDecision;
    public AngelDecision angelDecision;
    [SerializeField] public GameObject cardAngelAnswer;
    [SerializeField] public GameObject cardDemonAnswer;


    [Serializable]
    public class DemonDecision{

        public int moneyImpact;
        public int psycheImpact;
        public int popularityImpact;

        public DemonDecision(int moneyImpact, int psycheImpact, int popularityImpact){
            this.moneyImpact = moneyImpact;
            this.psycheImpact = psycheImpact;
            this.popularityImpact = popularityImpact;
        }
    }

    [Serializable]
    public class AngelDecision{

        public int moneyImpact;
        public int psycheImpact;
        public int popularityImpact;

        public AngelDecision(int moneyImpact, int psycheImpact, int popularityImpact){
            this.moneyImpact = moneyImpact;
            this.psycheImpact = psycheImpact;
            this.popularityImpact = popularityImpact;
        }
    }


    // Start is called before the first frame update`
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
