using System.Collections;
using UnityEngine;

namespace CardBehaviors.Implementations
{
    
    public class PointPlayBehavior : PlayBehavior
    {
        [SerializeField] private int pointsToAdd;
        
        public override void Play()
        {
            card.GetPlayer().SetActionable(false);
            StartCoroutine(playCoroutine());
        }
        
        public override bool CanPlay()
        {
            return true;
        }
        
        IEnumerator playCoroutine()
        {
            Player player = card.GetPlayer();
            card.playText.SetActive(true);
            player.AddScore(pointsToAdd);

            yield return new WaitForSeconds(2);
            
            card.Discard();
            player.SetActionable(true);
        }
    }
}