using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CPUDecisionMaker : MonoBehaviour
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
            Player.GetPlayer(this).TurnOver();
        }
        else
        {
            // Number of cards * 5 + 50% chance to play a card
            int random = Random.Range(0, 100);
            if (random < cards * 5 + 50)
            {
                int randomCard = UnityEngine.Random.Range(0, player.GetPlayerHand().CountCards());
                player.GetPlayerHand().PlayCard(randomCard);
                StartCoroutine(DecideCoroutine(player));
            }
            else
            {
                Player.GetPlayer(this).TurnOver();
            }
        }
    }
}