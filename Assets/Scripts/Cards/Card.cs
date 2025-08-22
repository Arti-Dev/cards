using UnityEngine;

public class Card : MonoBehaviour
{
    // Define which player state this card belongs to
    protected Player player = null;
    private bool moveable = true;
    private bool hasBeenPlayed = false;
    
    private bool lerping = true;
    private float lerpTime = 0;
    private const float lerpIncrement = 1 / 60f;
    private Vector3 startTransform = Vector3.zero;
    private Vector3 targetTransform = Vector3.zero;
    
    [SerializeField] public LayerMask dropLayermask;
    [SerializeField] private Sprite faceDownSprite;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite faceUpSprite;
    private string id;

    public void Play()
    {
        GetCardBehaviorObject().Play();
        hasBeenPlayed = true;
    }
    
    public void Discard()
    {
        GetCardBehaviorObject().Discard(player.GetDiscardPile());
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = Player.GetPlayer(this);
    }

    public void Show()
    {
        spriteRenderer.sprite = faceUpSprite;
    }
    
    public void Hide()
    {
        spriteRenderer.sprite = faceDownSprite;
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

    // Uses global position
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
    
    public bool HasBeenPlayed()
    {
        return hasBeenPlayed;
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

    public void SetId(string newId)
    {
        id = newId;
    }
    
    public string GetId()
    {
        return id;
    }

    public void SetScale(float scale)
    {
        transform.localScale = new Vector3(scale, scale, scale);
    }
}
