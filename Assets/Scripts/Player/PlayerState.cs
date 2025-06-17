using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour
{
    [SerializeField] private int startingScore = 50;
    private int score;

    [SerializeField] private bool isHuman;

    [SerializeField] private GameObject scoreTextObject = null;
    private TMP_Text scoreText = null;

    [SerializeField] private PlayingArea area = null;
    [SerializeField] private PlayerHand hand = null;
    [SerializeField] private Deck deck = null;
    [SerializeField] private DiscardPile discardPile = null;

    private bool turn = true;

    [SerializeField] private PlayerState opponentState;
    [SerializeField] private CPUPlayer cpu;

    // The direction to push cards if we draw a card and another one is already there
    // [SerializeField] Vector3 shiftVector;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score = startingScore;
        // Sometimes the Deck's Start() method runs after this method for some reason, so set the reference now
        deck.SetPlayerState();
        StartCoroutine(StartingCoroutine());
    }

    IEnumerator StartingCoroutine()
    {
        yield return new WaitForSeconds(0.3f);
        for (int i = 0; i < 3; i++)
        {
            deck.DrawRandomCard();
            yield return new WaitForSeconds(0.3f);
        }
        if (isHuman) turn = true;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText = scoreTextObject.GetComponent<TMP_Text>();
    }

    public void addScore(int delta)
    {
        if (score < 0) return;
        score += delta;
        scoreText.text = $"Score: {score}";
    }

    public void removeScore(int delta)
    {
        if (score < 0) return;
        score -= delta;
        scoreText.text = $"Score: {score}";
    }

    public static PlayerState GetPlayerState(MonoBehaviour obj)
    {
        return obj.GetComponentInParent<PlayerState>();
    }

    public PlayingArea GetPlayingArea()
    {
        return area;
    }

    public PlayerHand GetPlayerHand()
    {
        return hand;
    }

    public DiscardPile GetDiscardPile()
    {
        return discardPile;
    }

    public Deck GetDeck()
    {
        return deck;
    }

    public bool IsHuman()
    {
        return isHuman;
    }

    public void SignalToOpponent()
    {
        if (isHuman) Debug.Log("Opponent's turn...");
        opponentState.TurnStart();
    }

    public void TurnStart()
    {
        turn = true;
        if (isHuman) Debug.Log("It's your turn!");

        if (cpu != null)
        {
            cpu.Decide(this);
        }
    }

    public void TurnOver()
    {
        turn = false;
    }

    public bool IsTurn()
    {
        return turn;
    }

}
