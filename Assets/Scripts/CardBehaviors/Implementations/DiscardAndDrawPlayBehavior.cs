using System.Collections;
using UnityEngine;

namespace CardBehaviors.Implementations
{
    public class DiscardAndDrawPlayBehavior : PlayBehavior
    {
        [SerializeField] private int cardsToDiscard = 1;
        [SerializeField] private int cardsToDraw = 2;
        
        private int cardsDiscarded = 0;
        
        public override bool Play()
        {
            Player player = card.GetPlayer();
            if (player.GetPlayerHand().CountCards() < cardsToDiscard)
            {
                Debug.Log("Not enough cards in hand to play this card!");
                return false;
            }

            if (player.HasStarCardPlayedThisTurn())
            {
                Debug.Log("A star card has already been played this turn!");
                return false;
            }
            player.SetActionable(false);
            player.SetStarCardPlayedThisTurn(true);
            card.SetText($"Discard {cardsToDiscard}\nDraw {cardsToDraw}");
            card.text.SetActive(true);
            Debug.Log($"Click {cardsToDiscard} card(s) in your hand to discard.");
            DragCards.CardClickEvent += OnCardClick;
            return true;
        }

        void OnCardClick(DragCards.CardClickEventData data)
        {
            Card targetCard = data.card;
            if (card == targetCard) return;
            Player player = targetCard.GetPlayer();
            if (player != card.GetPlayer()) return;
            targetCard.Discard();
            
            cardsDiscarded++;
            if (cardsDiscarded < cardsToDiscard)
            {
                Debug.Log($"{cardsToDiscard - cardsDiscarded} more card(s) to discard");
            }
            else
            {
                DragCards.CardClickEvent -= OnCardClick;
                StartCoroutine(DrawAndEnd());
            }
        }

        IEnumerator DrawAndEnd()
        {
            yield return new WaitForSeconds(0.5f);
            Player player = card.GetPlayer();
            for (int i = 0; i < cardsToDraw; i++)
            {
                player.GetDeck().DrawTopCard();
                yield return new WaitForSeconds(0.5f);
            }
            card.Discard();
            player.SetActionable(true);
            cardsDiscarded = 0;
        }
    }
}