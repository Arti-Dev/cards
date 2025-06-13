using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour
{
    [SerializeField] private int startingScore = 50;
    private int score;

    [SerializeField] private GameObject scoreTextObject = null;
    private TMP_Text scoreText = null;

    [SerializeField] private PlayingArea area = null;
    [SerializeField] private PlayerHand hand = null;
    //[SerializeField] private Deck deck = null;

    [SerializeField] private DiscardPile discardPile = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score = startingScore;
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


}
