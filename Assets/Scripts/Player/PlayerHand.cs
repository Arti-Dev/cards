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

    public void PlayRandomCard()
    {
        int randomCard = UnityEngine.Random.Range(0, CountCards());
        Card card = transform.GetChild(randomCard).GetComponent<Card>();
        PlayerState.GetPlayerState(this).GetPlayingArea().PlayCard(card);
    }

    public void PlayCard(int index)
    {
        Card card = transform.GetChild(index).GetComponent<Card>();
        PlayerState.GetPlayerState(this).GetPlayingArea().PlayCard(card);
    }

    // public Vector3 calculateLandingSpot()
    // {
    //     Transform[] children = GetComponentsInChildren<Transform>();
    // }
}
