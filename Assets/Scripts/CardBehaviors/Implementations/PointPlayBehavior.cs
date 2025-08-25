using System.Collections;
using UnityEngine;

namespace CardBehaviors.Implementations
{
    
    public class PointPlayBehavior : PlayBehavior
    {
        [SerializeField] private int pointsToAdd;
        
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
            player.AddScore(pointsToAdd);

            yield return new WaitForSeconds(2);
            
            card.Discard();
            player.SetActionable(true);
        }
    }
}