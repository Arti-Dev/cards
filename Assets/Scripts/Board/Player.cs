using System.Collections;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Game game = null;
    [SerializeField] private int startingScore = 50;
    private int score;

    [SerializeField] private bool isHuman;

    [SerializeField] private GameObject scoreTextObject = null;
    private TMP_Text scoreText = null;

    [SerializeField] private PlayingArea playingArea = null;
    [SerializeField] private Hand hand = null;
    [SerializeField] private Deck deck = null;
    [SerializeField] private DiscardPile discardPile = null;

    private bool turn;
    
    [SerializeField] private CPUPlayer cpu;

    [SerializeField] private CardDatabase cardDatabase;

    // The direction to push cards if we draw a card and another one is already there
    // [SerializeField] Vector3 shiftVector;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score = startingScore;
        // Sometimes the Deck's Start() method runs after this method for some reason, so set the reference now
        deck.SetPlayer();
        StartCoroutine(StartingCoroutine());
        scoreText = scoreTextObject.GetComponent<TMP_Text>();
    }

    IEnumerator StartingCoroutine()
    {
        yield return new WaitForSeconds(0.3f);
        for (int i = 0; i < 3; i++)
        {
            deck.DrawRandomCard();
            yield return new WaitForSeconds(0.3f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore(int delta)
    {
        if (score < 0) return;
        score += delta;
        scoreText.text = $"Score: {score}";
    }

    public void RemoveScore(int delta)
    {
        if (score < 0) return;
        score -= delta;
        scoreText.text = $"Score: {score}";
    }

    public static Player GetPlayer(MonoBehaviour obj)
    {
        return obj.GetComponentInParent<Player>();
    }

    public PlayingArea GetPlayingArea()
    {
        return playingArea;
    }

    public Hand GetPlayerHand()
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

    public void TurnStart()
    {
        turn = true;
        if (isHuman) Debug.Log("It's your turn!");

        if (cpu)
        {
            Debug.Log("Opponent's turn...");
            cpu.Decide(this);
        }
    }

    public void TurnOver()
    {
        turn = false;
        game.NextTurn(this);
    }

    public bool IsTurn()
    {
        return turn;
    }
    
    public Player GetOpponent()
    {
        if (game) return game.GetCpuPlayer1();
        return null;
    }
    
    public void SetGame(Game game)
    {
        this.game = game;
    }

}
