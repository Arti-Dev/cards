using System;
using UnityEngine;

public abstract class Card : MonoBehaviour
{
    // Define which player state this card belongs to
    protected PlayerState playerState = null;
    private bool dragging = false;
    private bool lerping = true;
    private float lerpTime = 0;
    private const float lerpIncrement = 1/60f;
    private Vector3 startTransform = Vector3.zero;
    private Vector3 targetTransform = Vector3.zero;
    private Vector3 offset = Vector3.zero;
    [SerializeField] private LayerMask dropLayermask;

    public abstract void play();
    public abstract int get_id();

    void OnMouseDown()
    {
        Debug.Log("Click");
        if (lerping) return;
        // offset between center and actual mouse click
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dragging = true;
    }

    void OnMouseUp()
    {
        dragging = false;
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
                Discard(playerState.GetDiscardPile());
            }
        }

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        playerState = PlayerState.GetPlayerState(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (dragging)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
        }
        else if (lerping)
        {
            lerpTime += lerpIncrement;
            transform.position = Vector3.Lerp(startTransform, targetTransform, lerpTime);
            if (lerpTime >= 1)
            {
                lerping = false;
                lerpTime = 0;
            }
        }
    }

    protected void Discard(DiscardPile pile)
    {
        pile.UpdateTexture(this);
        Destroy(gameObject);
    }

    public void TransformLerp(Vector3 endPosition)
    {
        startTransform = transform.position;
        targetTransform = endPosition;
        lerping = true;
    }
}
