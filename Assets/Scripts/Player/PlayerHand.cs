using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
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

    public void PlayCard(int index)
    {
        Card card = transform.GetChild(index).GetComponent<Card>();
        Player.GetPlayer(this).GetPlayingArea().PlayCard(card);
    }

    // public Vector3 calculateLandingSpot()
    // {
    //     Transform[] children = GetComponentsInChildren<Transform>();
    // }
}
