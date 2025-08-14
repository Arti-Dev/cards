using UnityEngine;
using System.Collections.Generic;
using UnityEditor.Rendering;

public class Deck : MonoBehaviour
{
    // Define which player state this card belongs to
    protected Player player = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
        if (!player.IsTurn() || !player.IsActionable())
        {
            Debug.Log("It's not your turn!");
            return;
        }
        DrawRandomCard();
        Player.GetPlayer(this).TurnOver();
    }

    public void DrawRandomCard()
    {
        Card card = CardDatabase.InstantiateRandomCard(transform);
        card.Init();
        card.transform.SetParent(transform, false);
        card.transform.SetParent(player.GetPlayerHand().transform, true);
        card.TransformLerp(player.GetPlayerHand().transform.position);
        if (card.GetPlayer().IsHuman()) card.Show();
    }
}
