using System.Collections;
using UnityEngine;

namespace CardBehaviors.Implementations
{
    public class DefaultDiscardBehavior : DiscardBehavior
    {
        public override void Discard()
        {
            Player player = card.GetPlayer();
            player.SetActionable(true);
            player.GetDiscardPile().PushCard(card);
            
            card.boardBehavior.UnregisterEvents();
            card.SetDestroyWhenLerpComplete(true);
            if (card.text) card.text.SetActive(false);
            card.TransformLerp(player.GetDiscardPile().transform.position);
        }
    }
}