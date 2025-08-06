using UnityEngine;

public class BlankCard : CardBehavior
{

    public override void play()
    {
        PlayerState playerState = GetCardObject().GetPlayerState();
        Card card = GetCardObject();
        card.TransformLerp(playerState.GetDiscardPile().transform.position);
        Discard(playerState.GetDiscardPile());
    }

    public override string get_id()
    {
        return "blankCard";
    }


}
