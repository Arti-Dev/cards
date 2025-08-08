using UnityEngine;
using System.Collections.Generic;

public class Deck : MonoBehaviour
{
    // Define which player state this card belongs to
    protected Player playerState = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetPlayer();
    }

    public void SetPlayer()
    {
        playerState = Player.GetPlayer(this);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        DrawRandomCard();
        Player.GetPlayer(this).TurnOver();
        Player.GetPlayer(this).SignalToOpponent();
    }

    public void DrawRandomCard()
    {
        Card card = CardDatabase.InstantiateRandomCard();
        card.Init();
        card.transform.SetParent(transform, false);
        card.transform.SetParent(playerState.GetPlayerHand().transform, true);
        card.TransformLerp(playerState.GetPlayerHand().transform.position);
        if (card.GetPlayer().IsHuman()) card.Show();
    }
}
