using UnityEngine;
using System.Collections.Generic;
using Cards;
using UnityEditor.Rendering;

public class Deck : MonoBehaviour
{
    // Define which player state this card belongs to
    protected Player player = null;
    Stack<string> cardIds = new Stack<string>();
    [SerializeField] private CardDeck staticDeck = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LoadDeck();
        SetPlayer();
    }

    public void SetPlayer()
    {
        player = Player.GetPlayer(this);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        if (cardIds.Count == 0)
        {
            Debug.Log("Deck is empty!");
            return;
        }
        if (!player.IsTurn() || !player.IsActionable())
        {
            Debug.Log("It's not your turn!");
            return;
        }
        
        DrawTopCard();
        Player.GetPlayer(this).TurnOver();
    }

    public void DrawTopCard()
    {
        Card card = CardDatabase.InstantiateCard(cardIds.Pop(), transform);
        card.Init();
        player.GetPlayerHand().AddCard(card);
    }

    public void LoadDeck()
    {
        if (staticDeck == null) return;
        cardIds.Clear();
        foreach (var entry in staticDeck.cards)
        {
            for (var i = 0; i < entry.count; i++)
            {
                cardIds.Push(entry.cardName);
            }
        }
        ShuffleDeck();
    }

    // Fisher-Yates shuffle from https://stackoverflow.com/questions/2094239/swap-two-items-in-listt
    // Swap by deconstruction from https://dariuszwozniak.net/blog/swap-via-deconstruction
    private void ShuffleDeck()
    {
        var values = cardIds.ToArray();
        cardIds.Clear();
        var random = new System.Random();
        var n = values.Length;
        while (n > 1)
        {
            n--;
            var k = random.Next(n + 1);
            (values[k], values[n]) = (values[n], values[k]);
        }
        
        foreach (var value in values)
        {
            cardIds.Push(value);
        }
    }
}
