namespace CardBehaviors.Implementations
{
    public class DefaultPlayBehavior : PlayBehavior
    {
        public override void Play()
        {
            card.Discard();
        }
        
        public override bool CanPlay()
        {
            return true;
        }
    }
}