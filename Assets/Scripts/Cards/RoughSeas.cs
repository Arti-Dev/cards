using System.Collections;
using UnityEngine;

namespace Cards
{
    public class RoughSeas : CardBehavior
    {
        
        public override void Play()
        {
            Card card = GetCardObject();
            card.GetPlayer().SetActionable(false);
            StartCoroutine(playCoroutine());
        }
        
        IEnumerator playCoroutine()
        {
            Card card = GetCardObject();
            Player player = GetCardObject().GetPlayer();
            PlayingArea playingArea = player.GetPlayingArea();
            yield return new WaitForSeconds(2);
            
            playingArea.AddBoardCard(card);
            player.SetActionable(true);
            
        }
    }
}