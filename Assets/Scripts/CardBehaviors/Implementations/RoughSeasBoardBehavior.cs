namespace CardBehaviors.Implementations
{
    public class RoughSeasBoardBehavior : BoardBehavior
    {
        public override void RegisterEvents()
        {
            Player.TurnStartEvent += TurnStartEvent;
        }
        
        public override void UnregisterEvents()
        {
            Player.TurnStartEvent -= TurnStartEvent;
        }
        
        public void TurnStartEvent(Player player)
        {
            if (card.GetPlayer() != player) return;
            player.GetDeck().DrawTopCard();
        }
    }
}