using System.Collections;
using UnityEngine;

namespace CardBehaviors.Implementations
{
    public class DrawCardPlayBehavior : PlayBehavior
    {
        [SerializeField] private int cardsToDraw = 1;

        public override void Play()
        {
            Player player = card.GetPlayer();
            player.SetActionable(false);
            card.SetText($"Draw {cardsToDraw}");
            card.playText.SetActive(true);
            StartCoroutine(DrawAndEnd());
        }
        
        public override bool CanPlay()
        {
            return true;
        }
        
        IEnumerator DrawAndEnd()
        {
            yield return new WaitForSeconds(0.5f);
            Player player = card.GetPlayer();
            for (int i = 0; i < cardsToDraw; i++)
            {
                player.GetDeck().DrawTopCard();
                yield return new WaitForSeconds(0.5f);
            }
            card.Discard();
            card.playText.SetActive(false);
            player.SetActionable(true);
        }
    }
}