using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class PlayingArea : MonoBehaviour
{

    private List<Vector3> boardCardRelativeLocations = new List<Vector3>();

    private Card[] boardCards;
    
    // todo make this cancellable
    public delegate void PlayAction(Card card);
    public static event PlayAction CardPlayEvent;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (boardCardRelativeLocations.Count == 0)
        {
            boardCardRelativeLocations.Add(new Vector3(-5f, 1.5f, 0));
            boardCardRelativeLocations.Add(new Vector3(-5f, -2.5f, 0));
            boardCardRelativeLocations.Add(new Vector3(5f, 1.5f, 0));
            boardCardRelativeLocations.Add(new Vector3(5f, -2.5f, 0));
        }

        boardCards = new Card[boardCardRelativeLocations.Count];
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool PlayCard(Card card)
    {
        Player player = card.GetPlayer();
        MoveCardToCenter(card);
        
        if (!card.Play())
        {
            card.transform.SetParent(player.GetPlayerHand().transform);
            return false;
        }
        
        card.Show();
        player.GetPlayerHand().UpdateCardLocations();
        
        return true;
    }

    private void MoveCardToCenter(Card card)
    {
        card.transform.SetParent(transform, true);
        card.TransformLerp(transform.position);
    }

    public void AddBoardCard(Card card)
    {
        int slot = 0;
        while (slot < boardCards.Length && boardCards[slot] != null)
        {
            slot++;
        }

        if (slot >= boardCards.Length)
        {
            Debug.Log("No board slots available!");
            return;
        }
        card.transform.SetParent(transform, true);
        card.SetScale(0.5f);
        Vector3 location = transform.TransformPoint(boardCardRelativeLocations[slot]);
        card.TransformLerp(location);
        boardCards[slot] = card;
    }
}
