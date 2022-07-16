using System.Xml;
using System.Xml.Serialization;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
[XmlRoot("GameData")]
public class GameSaveData
{
    public int money;
    public int popularity;
    public int stress;
    public int step;
    public List<int> cardIds;

    public GameSaveData(int money, int popularity, int stress, int step, List<int> cardIds){
        this.money = money;
        this.popularity = popularity;
        this.stress = stress;
        this.step = step;
        this.cardIds = cardIds;
    }
}