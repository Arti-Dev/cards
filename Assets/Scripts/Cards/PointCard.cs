using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PointCard : CardBehavior
{

    [SerializeField] GameObject text = null;


    protected override void Start()
    {
        base.Start();
        text.SetActive(false);
    }
    public override void Play()
    {
        Card card = GetCardObject();
        card.GetPlayer().SetActionable(false);
        StartCoroutine(playCoroutine());
    }

    IEnumerator playCoroutine()
    {
        Player playerState = GetCardObject().GetPlayer();
        Card card = GetCardObject();
        text.SetActive(true);
        playerState.AddScore(5);

        yield return new WaitForSeconds(2);

        card.TransformLerp(playerState.GetDiscardPile().transform.position);
        text.SetActive(false);
        yield return new WaitForSeconds(1);
        Discard(playerState.GetDiscardPile());
        GetCardObject().GetPlayer().SetActionable(true);
    }
}
