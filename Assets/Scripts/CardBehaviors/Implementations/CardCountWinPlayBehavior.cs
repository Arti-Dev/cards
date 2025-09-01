using System.Collections;
using UnityEngine;

namespace CardBehaviors.Implementations
{
    public class CardCountWinPlayBehavior : PlayBehavior
    {
        [SerializeField] private int requiredCards = 100;
        
        public override bool CanPlay()
        {
            return true;
        }
        
        public override void Play()
        {
            card.GetPlayer().SetActionable(false);
            card.playText.SetActive(true);
            card.StartCoroutine(Coroutine());
            
        }

        private IEnumerator Coroutine()
        {
            Hand hand = card.GetPlayer().GetPlayerHand();
            Deck deck = card.GetPlayer().GetDeck();
            DiscardPile discardPile = card.GetPlayer().GetDiscardPile();
            int totalCards = hand.CountCards() + deck.Count() + discardPile.Count();
            
            Game.Log($"{hand.CountCards()} from hand + {deck.Count()} from deck + {discardPile.Count()} from discard pile = {totalCards} total cards");
            yield return new WaitForSeconds(3);
            if (totalCards >= requiredCards)
            {
                Game.PlayerWin();
            }
            else
            {
                Game.Log($"Not enough cards to win (needed {requiredCards})!");
                card.Discard();
                card.GetPlayer().SetActionable(true);
            }
            card.playText.SetActive(false);
        }
    }
}