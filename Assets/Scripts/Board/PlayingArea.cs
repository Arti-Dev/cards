using UnityEngine;

public class PlayingArea : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayCard(Card card)
    {
        // todo check if there's already a card in play
        Player player = card.GetPlayer();
        MoveCardHere(card);
        player.GetPlayerHand().UpdateCardLocations();
        card.Show();
        card.Play();
        // todo if there are too many cards here some cards will become permanently unmovable
    }

    void MoveCardHere(Card card)
    {
        card.transform.SetParent(transform, true);
        card.TransformLerp(transform.position);
    }
}
