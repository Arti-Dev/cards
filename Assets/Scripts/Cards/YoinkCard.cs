using System.Collections;
using UnityEngine;

public class YoinkCard : CardBehavior
{

    [SerializeField] GameObject text = null;


    protected override void Start()
    {
        base.Start();
        text.SetActive(false);
    }
    public override void Play()
    {
        GetCardObject().GetPlayer().TurnOver();
        StartCoroutine(playCoroutine());

    }

    IEnumerator playCoroutine()
    {
        Player playerState = GetCardObject().GetPlayer();
        Card card = GetCardObject();
        text.SetActive(true);
        // todo check if score is actually above 10
        playerState.GetOpponent().removeScore(10);
        playerState.addScore(10);

        yield return new WaitForSeconds(2);

        card.TransformLerp(playerState.GetDiscardPile().transform.position);
        text.SetActive(false);
        yield return new WaitForSeconds(1);
        Discard(playerState.GetDiscardPile());
    }

    public override string GetId()
    {
        return "yoinkCard";
    }


}
