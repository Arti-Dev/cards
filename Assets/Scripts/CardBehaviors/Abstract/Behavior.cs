using System;
using UnityEngine;

namespace CardBehaviors
{
    public abstract class Behavior : MonoBehaviour
    {
        protected Card card;

        protected virtual void Awake()
        {
            card = GetComponent<Card>();
            if (card == null)
            {
                Debug.LogError("Behavior script must be attached to a GameObject with a Card component.");
            }
        }
    }
}