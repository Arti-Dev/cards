using CardBehaviors;
using TMPro;
using UnityEngine;

public class Card : MonoBehaviour
{
    // Define which player state this card belongs to
    protected Player player = null;
    private bool moveable = true;
    private bool playable = true;
    private bool hasBeenPlayed = false;
    
    private bool lerping = true;
    private float lerpTime = 0;
    private const float lerpIncrement = 1 / 60f;
    private Vector3 startTransform = Vector3.zero;
    private Vector3 targetTransform = Vector3.zero;
    
    [SerializeField] private Sprite faceDownSprite;
    private Sprite faceUpSprite;
    private SpriteRenderer spriteRenderer;
    [SerializeField] public GameObject text;
    private bool destroyWhenLerpComplete = false;
    
    private string id;
    [SerializeField] private bool starCard = false;

    [SerializeField] public BoardBehavior boardBehavior = null;
    [SerializeField] public PlayBehavior playBehavior = null;
    [SerializeField] public VisualBehavior visualBehavior = null;
    [SerializeField] public DiscardBehavior discardBehavior = null;

    public bool Play()
    {
        if (!playable) return false;
        hasBeenPlayed = playBehavior.Play();
        return hasBeenPlayed;
    }
    
    public void Discard()
    {
        discardBehavior.Discard();
    }

    void Awake()
    {
        player = Player.GetPlayer(this);
        spriteRenderer = GetComponent<SpriteRenderer>();
        faceUpSprite = spriteRenderer.sprite;
        if (text) text.SetActive(false);
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
                if (destroyWhenLerpComplete)
                {
                    Destroy(gameObject);
                }
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

    public void SetText(string str)
    {
        if (!text) return;
        text.GetComponent<TextMeshProUGUI>().SetText(str);
    }

    public bool CanMove()
    {
        return moveable;
    }
    
    public bool HasBeenPlayed()
    {
        return hasBeenPlayed;
    }

    public Player GetPlayer()
    {
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
    
    public void SetDestroyWhenLerpComplete(bool destroy)
    {
        destroyWhenLerpComplete = destroy;
    }
    
    public bool IsStarCard()
    {
        return starCard;
    }
    
    public bool IsPlayable()
    {
        return playable;
    }
    
    /**
     * Makes this card unplayable for the rest of this turn, but enables it next turn.
     */
    public void SetUnplayableThisTurn()
    {
        if (!playable) return;
        playable = false;
        Player.TurnStartEvent += OnNewTurnSetPlayable;
    }

    private void OnNewTurnSetPlayable(Player p)
    {
        if (p != player) return;
        Player.TurnStartEvent -= OnNewTurnSetPlayable;
        playable = true;
    }
}
