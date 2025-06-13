using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PointCard : Card
{

    [SerializeField] GameObject text = null;


    protected override void Start()
    {
        base.Start();
        text.SetActive(false);
    }
    public override void play()
    {
        StartCoroutine(playCoroutine());

    }

    IEnumerator playCoroutine()
    {
        text.SetActive(true);
        playerState.addScore(5);

        yield return new WaitForSeconds(2);

        TransformLerp(playerState.GetDiscardPile().transform.position);
        text.SetActive(false);
        yield return new WaitForSeconds(1);
        Discard(playerState.GetDiscardPile());
    }

    public override int get_id()
    {
        return 1;
    }


}
