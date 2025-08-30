using System.Collections;
using UnityEngine;

namespace CardBehaviors.Implementations
{
    public class DrawCardPlayBehavior : PlayBehavior
    {
        [SerializeField] private int cardsToDraw = 1;

        public override bool Play()
        {
            Player player = card.GetPlayer();
            player.SetActionable(false);
            card.SetText($"Draw {cardsToDraw}");
            card.playText.SetActive(true);
            StartCoroutine(DrawAndEnd());
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
            player.SetActionable(true);
        }
    }
}