using System;
using UnityEngine;

public class Card : MonoBehaviour
{
    // Define which player state this card belongs to
    protected Player player = null;
    private bool moveable = true;
    private bool lerping = true;
    private float lerpTime = 0;
    private const float lerpIncrement = 1 / 60f;
    private Vector3 startTransform = Vector3.zero;
    private Vector3 targetTransform = Vector3.zero;
    private Vector3 offset = Vector3.zero;
    [SerializeField] public LayerMask dropLayermask;
    [SerializeField] private Sprite faceDownSprite;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite faceUpSprite;

    public void Play()
    {
        GetCardBehaviorObject().Play();
    }
    public string GetID()
    {
        return GetCardBehaviorObject().GetId();
    }

    public void OnDrop()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero,
        float.PositiveInfinity, dropLayermask);
        if (!hit) return;
        
        if (!player.IsTurn())
        {
            Debug.Log("It's not your turn!");
            return;
        }
        if (!player.IsActionable())
        {
            Debug.Log("You can't play a card right now!");
            return;
        }
        
        Type type = hit.collider.gameObject.GetComponent<MonoBehaviour>().GetType();
        if (type == typeof(PlayingArea))
        {
            player.GetPlayingArea().PlayCard(this);
        }
        else if (type == typeof(DiscardPile))
        {
            GetCardBehaviorObject().Discard(player.GetDiscardPile());
        }

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        player = Player.GetPlayer(this);
    }

    // We shouldn't need this method, but when I instantiate this class from the Deck the Start() doesn't run in time!
    public void Init()
    {
        player = Player.GetPlayer(this);
        spriteRenderer.sprite = faceDownSprite;
    }

    public void Show()
    {
        spriteRenderer.sprite = faceUpSprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (lerping)
        {
            lerpTime += lerpIncrement;
            transform.position = Vector3.Lerp(startTransform, targetTransform, lerpTime);
            if (lerpTime >= 1)
            {
                lerping = false;
                moveable = true;
                lerpTime = 0;
            }
        }
    }

    public void TransformLerp(Vector3 endPosition)
    {
        startTransform = transform.position;
        targetTransform = endPosition;
        lerping = true;
        moveable = false;
    }

    public bool CanMove()
    {
        return moveable;
    }

    protected CardBehavior GetCardBehaviorObject()
    {
        return gameObject.GetComponent<CardBehavior>();
    }

    public Player GetPlayer()
    {
        if (player == null) return Player.GetPlayer(this);
        return player;
    }

    public SpriteRenderer GetSpriteRenderer()
    {
        return spriteRenderer;
    }
}
