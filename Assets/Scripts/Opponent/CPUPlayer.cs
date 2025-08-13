using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CPUPlayer : MonoBehaviour
{

    public void Decide(Player player)
    {
        StartCoroutine(DecideCoroutine(player));
    }

    IEnumerator DecideCoroutine(Player player)
    {
        yield return new WaitForSeconds(2);
        int cards = player.GetPlayerHand().CountCards();
        if (cards < 2)
        {
            player.GetDeck().DrawRandomCard();
            Player.GetPlayer(this).TurnOver();
        }
        else
        {
            // 50/50 to play a random card or draw another card
            int random = UnityEngine.Random.Range(0, 2);
            if (random == 0)
            {
                int randomCard = UnityEngine.Random.Range(0, player.GetPlayerHand().CountCards());
                player.GetPlayerHand().PlayCard(randomCard);
            }
            else
            {
                player.GetDeck().DrawRandomCard();
                Player.GetPlayer(this).TurnOver();
            }
        }
    }
}