using UnityEngine;

public class BlankCard : CardBehavior
{

    public override void Play()
    {
        Player playerState = GetCardObject().GetPlayer();
        Card card = GetCardObject();
        card.TransformLerp(playerState.GetDiscardPile().transform.position);
        Discard(playerState.GetDiscardPile());
    }
}
