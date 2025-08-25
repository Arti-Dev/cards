namespace CardBehaviors.Implementations
{
    public class RoughSeasBoardBehavior : BoardBehavior
    {
        public override void RegisterEvents()
        {
            Player.OnTurnStart += OnTurnStart;
        }
        
        public override void UnregisterEvents()
        {
            Player.OnTurnStart -= OnTurnStart;
        }
        
        public void OnTurnStart(Player player)
        {
            if (card.GetPlayer() != player) return;
            player.GetDeck().DrawTopCard();
        }
    }
}