using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "CardDatabase", menuName = "ScriptableObjects/CardDatabase", order = 1)]
public class CardDatabase : ScriptableObject
{
    private static Dictionary<string, GameObject> cardDictionary = new Dictionary<string, GameObject>();
    public List<GameObject> cardList;

    public static GameObject InstantiateCard(string str)
    {
        if (!cardDictionary.ContainsKey(str)) return null;
        else return Instantiate(cardDictionary[str]);
    }

    public void initDictionary()
    {
        foreach (GameObject card in cardList)
        {
            if (card.GetComponent<Card>())
                cardDictionary[card.GetComponent<Card>().get_id()] = card;
        }
    }

    public static GameObject GetPrefab(string str)
    {
        return cardDictionary[str];
    }

}