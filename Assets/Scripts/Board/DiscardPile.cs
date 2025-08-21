using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DiscardPile : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer = null;
    Sprite emptySprite = null;
    private Player player = null;
    Stack<string> discardedCards = new Stack<string>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = Player.GetPlayer(this);
        emptySprite = spriteRenderer.sprite;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        Deck deck = player.GetDeck();
        if (deck == null)
        {
            Debug.Log("There is no deck??");
        }

        if (deck.Count() == 0)
        {
            deck.ShuffleDiscardPileIntoDeck(this);
        }
        else
        {
            Debug.Log("You can only shuffle your discard pile back into your deck when it is empty!");
        }
    }

    public void Discard(Card card)
    {
        spriteRenderer.sprite = card.GetSpriteRenderer().sprite;
        discardedCards.Push(card.GetId());
    }
    
    public Stack<string> GetDiscardedCards()
    {
        return discardedCards;
    }
    
    public void ClearDiscardPile()
    {
        discardedCards.Clear();
        spriteRenderer.sprite = emptySprite;
    }
}
