using System;
using UnityEngine;

public class Card : MonoBehaviour
{
    // Define which player state this card belongs to
    protected PlayerState playerState = null;
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

    public void play()
    {
        GetCardBehaviorObject().play();
    }
    public string get_id()
    {
        return GetCardBehaviorObject().get_id();
    }

    public void OnDrop()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero,
        float.PositiveInfinity, dropLayermask);

        if (hit)
        {
            Type type = hit.collider.gameObject.GetComponent<MonoBehaviour>().GetType();
            if (type == typeof(PlayingArea))
            {
                playerState.GetPlayingArea().PlayCard(this);
            }
            else if (type == typeof(DiscardPile))
            {
                GetCardBehaviorObject().Discard(playerState.GetDiscardPile());
            }
            return;
        }

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        playerState = PlayerState.GetPlayerState(this);
    }

    // We shouldn't need this method, but when I instantiate this class from the Deck the Start() doesn't run in time!
    public void Init()
    {
        playerState = PlayerState.GetPlayerState(this);
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
        
        return playerState.IsTurn() && moveable;
    }

    protected CardBehavior GetCardBehaviorObject()
    {
        return gameObject.GetComponent<CardBehavior>();
    }

    public PlayerState GetPlayerState()
    {
        if (playerState == null) return PlayerState.GetPlayerState(this);
        return playerState;
    }
}
