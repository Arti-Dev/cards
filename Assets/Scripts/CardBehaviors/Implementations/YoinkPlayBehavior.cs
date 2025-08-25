using System.Collections;
using UnityEngine;

namespace CardBehaviors.Implementations
{
    public class YoinkPlayBehavior : PlayBehavior
    {
        [SerializeField] private int pointsToSteal = 10;
        
        public override bool Play()
        {
            card.GetPlayer().SetActionable(false);
            StartCoroutine(playCoroutine());
            return true;
        }
        
        IEnumerator playCoroutine()
        {
            Player player = card.GetPlayer();
            card.text.SetActive(true);
            // todo check if score is actually above 10
            // todo allow choosing opponent if there are multiple
            player.GetOpponent().RemoveScore(10);
            player.AddScore(pointsToSteal);

            yield return new WaitForSeconds(2);

            card.TransformLerp(player.GetDiscardPile().transform.position);
            card.text.SetActive(false);
            yield return new WaitForSeconds(1);
            card.Discard();
            player.SetActionable(true);
        }
    }
}