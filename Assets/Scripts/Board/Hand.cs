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
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

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

    public void UpdateCardLocations(int startIndex, int endIndex)
    {
        for (var i = startIndex; i < endIndex; i++)
        {
            var card = transform.GetChild(i).GetComponent<Card>();
            if (card == null) continue;
            var newPosition = CalculateCardLocation(i);
            card.TransformLerp(newPosition);
            card.GetSpriteRenderer().sortingOrder = i;
        }
    }
    
    public void UpdateCardLocations()
    {
        UpdateCardLocations(0, transform.childCount);
    }
    

    private Vector3 CalculateCardLocation(int index) => alignment switch
    {
        Alignment.Left => transform.TransformPoint(new Vector3(-maxLeft + index * gap, 0, 0)),
        Alignment.Right => transform.TransformPoint(new Vector3(maxRight - index * gap, 0, 0)),
        _ => Vector3.zero
    };



    // public Vector3 calculateLandingSpot()
    // {
    //     Transform[] children = GetComponentsInChildren<Transform>();
    // }
}
