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
            player.SetActionable(false);
            card.SetText($"Discard {cardsToDiscard}\nDraw {cardsToDraw}");
            Debug.Log($"Click {cardsToDiscard} card(s) in your hand to discard.");
            DragCards.CardClickEvent += OnCardClick;
            return true;
        }

        void OnCardClick(DragCards.CardClickEventData data)
        {
            Card targetCard = data.card;
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
                StartCoroutine(DrawAndEnd());
            }
        }

        IEnumerator DrawAndEnd()
        {
            Player player = card.GetPlayer();
            for (int i = 0; i < cardsToDraw; i++)
            {
                player.GetDeck().DrawTopCard();
                yield return new WaitForSeconds(0.5f);
            }
            card.Discard();
            player.SetActionable(true);
            DragCards.CardClickEvent -= OnCardClick;
            cardsDiscarded = 0;
        }
    }
}