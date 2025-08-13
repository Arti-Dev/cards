using UnityEngine;
using UnityEngine.InputSystem;

public class DragAll : MonoBehaviour
{
    private Transform dragging = null;
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
        if (leftClickAction.IsPressed() && !dragging)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(mousePositionAction.ReadValue<Vector2>()), Vector2.zero,
        float.PositiveInfinity, dragMask);
            HandleHit(hit);
        }
        // release
        else if (!leftClickAction.IsPressed() && dragging != null)
        {
            Card card = dragging.GetComponent<Card>();
            if (card != null) card.OnDrop();

            dragging = null;
        }

        if (dragging != null)
        {
            dragging.position = Camera.main.ScreenToWorldPoint(mousePositionAction.ReadValue<Vector2>()) + offset;
        }
    }

    void HandleHit(RaycastHit2D hit)
    {
        if (!hit) return;
        Card card = hit.transform.GetComponent<Card>();
        if (!card) return;
        if (!card.CanMove() || !card.GetPlayer().IsHuman()) return;
        dragging = hit.transform;
        offset = dragging.position - Camera.main.ScreenToWorldPoint(mousePositionAction.ReadValue<Vector2>());
    }
}
