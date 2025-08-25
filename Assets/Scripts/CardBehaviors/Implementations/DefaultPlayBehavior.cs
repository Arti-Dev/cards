namespace CardBehaviors.Implementations
{
    public class DefaultPlayBehavior : PlayBehavior
    {
        public override bool Play()
        {
            card.Discard();
            return true;
        }
    }
}