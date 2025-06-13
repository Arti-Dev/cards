using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LosePointCard : Card
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
        playerState.removeScore(5);

        yield return new WaitForSeconds(2);

        TransformLerp(playerState.GetDiscardPile().transform.position);
        text.SetActive(false);
        yield return new WaitForSeconds(1);
        Discard(playerState.GetDiscardPile());
    }

    public override int get_id()
    {
        return 2;
    }


}
