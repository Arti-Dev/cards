using System.Collections;
using UnityEngine;

namespace Cards
{
    public class RoughSeas : CardBehavior
    {
        
        public override void Play()
        {
            Card card = GetCardObject();
            card.GetPlayer().SetActionable(false);
            StartCoroutine(playCoroutine());
        }
        
        IEnumerator playCoroutine()
        {
            Card card = GetCardObject();
            Player player = GetCardObject().GetPlayer();
            PlayingArea playingArea = player.GetPlayingArea();
            Player.OnTurnStart += OnTurnStart;
            yield return new WaitForSeconds(2);
            
            playingArea.AddBoardCard(card);
            player.SetActionable(true);
            
        }

        public override void Discard(DiscardPile pile)
        {
            Player.OnTurnStart -= OnTurnStart;
            base.Discard(pile);
        }

        public void OnTurnStart(Player player)
        {
            Card card = GetCardObject();
            if (card.GetPlayer() != player) return;
            player.GetDeck().DrawTopCard();
        }
    }
}