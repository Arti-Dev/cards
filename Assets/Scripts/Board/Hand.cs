using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    
    enum Alignment
    {
        Left,
        Right
    }

    // Maximum dimensions of the hand
    [SerializeField] private float maxLeft = 5;
    [SerializeField] private float maxRight = 5;
    [SerializeField] private Alignment alignment = Alignment.Left;
    private float gap = 1.5f;
    private float verticalOffset = 2.5f;
    private int maxPerRow = 6;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // todo temp
        if (transform.position.y > 0) verticalOffset = -verticalOffset;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int CountCards()
    {
        return transform.childCount;
    }

    public Card GetCard(int index)
    {
        if (index >= transform.childCount) return null;
        Card card = transform.GetChild(index).GetComponent<Card>();
        return card;
    }

    // todo break up and move to CPUPlayer class
    public void PlayCard(int index)
    {
        Card card = transform.GetChild(index).GetComponent<Card>();
        Player.GetPlayer(this).GetPlayingArea().PlayCard(card);
    }

    public void AddCard(Card card)
    {
        var relativePosition = CalculateCardLocation(transform.childCount);
        card.GetSpriteRenderer().sortingOrder = transform.childCount;
        card.transform.SetParent(transform, true);
        card.TransformLerp(relativePosition);
        if (card.GetPlayer().IsHuman()) card.Show();
    }
    
    private IEnumerator UpdateCardLocationsCoroutine()
    {
        yield return 0;
        for (var i = 0; i < transform.childCount; i++)
        {
            var card = transform.GetChild(i).GetComponent<Card>();
            if (!card) continue;
            var newPosition = CalculateCardLocation(i);
            card.TransformLerp(newPosition);
            card.GetSpriteRenderer().sortingOrder = i;
        }
    }

    public void UpdateCardLocations()
    {
        StartCoroutine(UpdateCardLocationsCoroutine());
    }

    private Vector3 CalculateCardLocation(int index)
    {
        int horizontal = index % maxPerRow;
        int vertical = index / maxPerRow;
        Vector3 point = Vector3.zero;
        switch (alignment)
        {
            case Alignment.Left:
                point = transform.TransformPoint(new Vector3(-maxLeft + horizontal * gap, 0, 0));
                point.y -= vertical * verticalOffset;
                break;
            case Alignment.Right:
                point = transform.TransformPoint(new Vector3(maxRight - horizontal * gap, 0, 0));
                point.y -= vertical * verticalOffset;
                break;
        }

        return point;
    }

    public void MergeCards()
    {
        Dictionary<string, List<Card>> cardsTypeSorted = new Dictionary<string, List<Card>>();
        foreach (Transform t in transform)
        {
            var card = t.GetComponent<Card>();
            if (!card.IsPlayable()) continue;
            var id = card.GetId();
            cardsTypeSorted.TryAdd(id, new List<Card>());
            cardsTypeSorted[id].Add(card);
        }

        // Merge cards with frequency > 1
        foreach (var pair in cardsTypeSorted)
        {
            var cardList = pair.Value;
            if (cardList.Count <= 1) continue;
            int totalStacks = 0;
            for (int i = 0; i < cardList.Count; i++)
            {
                var card = cardList[i];
                totalStacks += card.GetStacks();
                if (i != 0) Destroy(card.gameObject);
            }
            cardList[0].SetStacks(totalStacks);
        }
    }



    // public Vector3 calculateLandingSpot()
    // {
    //     Transform[] children = GetComponentsInChildren<Transform>();
    // }
}
