using UnityEngine;
using System.Collections.Generic;

public class Deck : MonoBehaviour
{
    // Define which player state this card belongs to
    protected PlayerState playerState = null;

    // index = card ID
    [SerializeField] public List<Card> allPossibleCards;

    public Card NewCardByID(int id)
    {
        if (id < 0 || id > allPossibleCards.Count - 1) return null;
        return allPossibleCards[id];
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetPlayerState();
    }

    public void SetPlayerState()
    {
        playerState = PlayerState.GetPlayerState(this);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        DrawRandomCard();
        PlayerState.GetPlayerState(this).TurnOver();
        PlayerState.GetPlayerState(this).SignalToOpponent();
    }

    public void DrawRandomCard()
    {
        int cardID = UnityEngine.Random.Range(0, allPossibleCards.Count);
        // Add a new card to the Hand that the PlayerState has
        Card card = Instantiate(NewCardByID(cardID), new Vector3(0, 0, 0), Quaternion.identity);
        card.Init();
        card.transform.SetParent(transform, false);
        card.transform.SetParent(playerState.GetPlayerHand().transform, true);
        card.TransformLerp(playerState.GetPlayerHand().transform.position);
        if (card.GetPlayerState().IsHuman()) card.Show();

        print(CardDatabase.GetPrefab(card.get_id()));
    }
}
