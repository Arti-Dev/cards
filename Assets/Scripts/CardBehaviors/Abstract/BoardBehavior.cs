using UnityEngine;

namespace CardBehaviors
{
    public abstract class BoardBehavior : Behavior
    {
        // Register events here
        public abstract void RegisterEvents();
        public abstract void UnregisterEvents();
    }
}