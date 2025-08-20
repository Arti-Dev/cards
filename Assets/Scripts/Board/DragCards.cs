using UnityEngine;
using UnityEngine.InputSystem;

public class DragCards : MonoBehaviour
{
    private Card draggingCard;
    private Vector3 originalPosition;
    private Vector3 offset;
    private InputAction leftClickAction;
    private InputAction mousePositionAction;
    [SerializeField] LayerMask dragMask;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        leftClickAction = InputSystem.actions.FindAction("LeftClick");
        mousePositionAction = InputSystem.actions.FindAction("MousePosition");
    }

    // Update is called once per frame
    void Update()
    {
        // click
        if (leftClickAction.IsPressed() && !draggingCard)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(mousePositionAction.ReadValue<Vector2>()), Vector2.zero,
        float.PositiveInfinity, dragMask);
            HandleHit(hit);
        }
        // release
        else if (!leftClickAction.IsPressed() && draggingCard != null)
        {
            if (draggingCard != null) draggingCard.OnDrop(originalPosition);
            draggingCard = null;
        }

        if (draggingCard != null)
        {
            draggingCard.transform.position = Camera.main.ScreenToWorldPoint(mousePositionAction.ReadValue<Vector2>()) + offset;
        }
    }

    void HandleHit(RaycastHit2D hit)
    {
        if (!hit) return;
        Card card = hit.transform.GetComponent<Card>();
        if (!card) return;
        if (!card.CanMove() || !card.GetPlayer().IsHuman()) return;
        draggingCard = card;
        offset = card.transform.position - Camera.main.ScreenToWorldPoint(mousePositionAction.ReadValue<Vector2>());
        originalPosition = card.transform.position;
    }
}
