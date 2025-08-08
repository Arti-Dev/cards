using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "CardDatabase", menuName = "ScriptableObjects/CardDatabase", order = 1)]
public class CardDatabase : ScriptableObject
{
    private static Dictionary<string, GameObject> cardDictionary = new Dictionary<string, GameObject>();
    public List<GameObject> cardList;

    public static Card InstantiateCard(string str)
    {
        if (!cardDictionary.ContainsKey(str)) return null;
        else return Instantiate(cardDictionary[str]).GetComponent<Card>();
    }

    public static Card InstantiateRandomCard()
    {
        int randomInt = UnityEngine.Random.Range(0, cardDictionary.Count);
        List<string> ids = Enumerable.ToList<string>(cardDictionary.Keys);
        return InstantiateCard(ids[randomInt]);
    }

    public void initDictionary()
    {
        foreach (GameObject card in cardList)
        {
            if (card.GetComponent<Card>())
                cardDictionary[card.GetComponent<Card>().GetID()] = card;
        }
    }

    public static int TotalCardCount()
    {
        return cardDictionary.Count;
    }


    public static GameObject GetPrefab(string str)
    {
        return cardDictionary[str];
    }

}