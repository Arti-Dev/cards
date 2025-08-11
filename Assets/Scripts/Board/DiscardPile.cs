using UnityEngine;

public class DiscardPile : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateTexture(Card card)
    {
        GetComponent<SpriteRenderer>().sprite = card.GetComponent<SpriteRenderer>().sprite;
    }
}
