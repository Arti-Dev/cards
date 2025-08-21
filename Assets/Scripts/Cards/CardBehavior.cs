using UnityEngine;

public abstract class CardBehavior : MonoBehaviour
{
    public abstract void Play();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        
    }

    public virtual void Discard(DiscardPile pile)
    {
        pile.Discard(GetCardObject());
        Destroy(gameObject);
    }

    protected Card GetCardObject()
    {
        return gameObject.GetComponent<Card>();
    }
}