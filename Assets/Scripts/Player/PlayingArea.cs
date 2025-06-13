using UnityEngine;

public class PlayingArea : MonoBehaviour
{
    PlayerState playerState;

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
        MoveCardHere(card);
        card.play();
        // todo if there are too many cards here some cards will become permanently unmovable
    }

    void MoveCardHere(Card card)
    {
        card.transform.SetParent(transform, true);
        card.TransformLerp(transform.position);
    }
}
