using UnityEngine;

public abstract class CardBehavior : MonoBehaviour
{
    public abstract void Play();
    public abstract string GetId();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        
    }

    public virtual void Discard(DiscardPile pile)
    {
        pile.UpdateTexture(GetCardObject());
        Destroy(gameObject);
        GetCardObject().GetPlayer().TurnOver();
        GetCardObject().GetPlayer().SignalToOpponent();
    }

    protected Card GetCardObject()
    {
        return gameObject.GetComponent<Card>();
    }
}