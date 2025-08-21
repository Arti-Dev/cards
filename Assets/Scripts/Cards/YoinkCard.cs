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
        Card card = GetCardObject();
        card.GetPlayer().SetActionable(false);
        StartCoroutine(playCoroutine());

    }

    IEnumerator playCoroutine()
    {
        Player player = GetCardObject().GetPlayer();
        Card card = GetCardObject();
        text.SetActive(true);
        // todo check if score is actually above 10
        // todo allow choosing opponent if there are multiple
        player.GetOpponent().RemoveScore(10);
        player.AddScore(10);

        yield return new WaitForSeconds(2);

        card.TransformLerp(player.GetDiscardPile().transform.position);
        text.SetActive(false);
        yield return new WaitForSeconds(1);
        Discard(player.GetDiscardPile());
        GetCardObject().GetPlayer().SetActionable(true);
    }
}
