using System.Collections;
using UnityEngine;

namespace CardBehaviors.Implementations
{
    public class BoardCardPlayBehavior : PlayBehavior
    {
        public override bool Play()
        {
            card.GetPlayer().SetActionable(false);
            StartCoroutine(playCoroutine());
            return true;
        }
        
        IEnumerator playCoroutine()
        {
            Player player = card.GetPlayer();
            PlayingArea playingArea = player.GetPlayingArea();
            card.boardBehavior.RegisterEvents();
            yield return new WaitForSeconds(2);
            
            playingArea.AddBoardCard(card);
            player.SetActionable(true);
        }
    }
}