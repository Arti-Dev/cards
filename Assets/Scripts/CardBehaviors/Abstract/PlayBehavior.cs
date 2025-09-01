using UnityEngine;

namespace CardBehaviors
{
    public abstract class PlayBehavior : Behavior
    {
        public abstract void Play();

        public abstract bool CanPlay();
    }
}