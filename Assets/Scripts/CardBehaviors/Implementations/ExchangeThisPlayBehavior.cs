using UnityEngine;

namespace CardBehaviors.Implementations
{
    public class ExchangeThisPlayBehavior : PlayBehavior
    {
        public override bool Play()
        {
            Player player = card.GetPlayer();
            Player teammate = player.GetTeammate();
            if (!teammate)
            {
                Debug.Log("You have no teammate!");
                return false;
            }
            
            if (teammate.GetPlayerHand().CountCards() == 0)
            {
                Debug.Log("Your teammate has no cards in hand to exchange with!");
                return false;
            }
            
            player.SetActionable(false);
            card.playText.SetActive(true);
            Debug.Log("Click a card in your teammate's hand to exchange with this card.");
            DragCards.CardClickEvent += OnCardClick;
            return true;
        }
        
        void OnCardClick(DragCards.CardClickEventData data)
        {
            Player player = card.GetPlayer();
            Player teammate = player.GetTeammate();
            Card targetCard = data.card;

            if (targetCard.GetPlayer() != teammate)
            {
                Debug.Log("This card belongs to " + targetCard.GetPlayer());
                return;
            }

            if (targetCard.HasBeenPlayed())
            {
                return;
            }
            
            targetCard.TransferToOtherPlayer(player);
            card.TransferToOtherPlayer(teammate);
            targetCard.SetUnplayableThisTurn();
            card.SetHasBeenPlayed(false);
            
            Debug.Log($"Exchanged {card.GetId()} with {targetCard.GetId()}");
            DragCards.CardClickEvent -= OnCardClick;
            
            card.playText.SetActive(false);
            player.SetActionable(true);
        }
    }
}