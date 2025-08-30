using System.Collections;
using UnityEngine;

namespace CardBehaviors.Implementations
{
    public class DefaultDiscardBehavior : DiscardBehavior
    {
        public override void Discard()
        {
            Player player = card.GetPlayer();
            card.transform.SetParent(player.GetDiscardPile().transform, true);
            player.GetPlayerHand().UpdateCardLocations();
            player.SetActionable(true);
            player.GetDiscardPile().PushCard(card);
            
            card.boardBehavior.UnregisterEvents();
            card.SetDestroyWhenLerpComplete(true);
            if (card.playText) card.playText.SetActive(false);
            card.TransformLerp(player.GetDiscardPile().transform.position);
        }
    }
}