using System.Collections;
using UnityEngine;

public class CPUPlayer : MonoBehaviour
{

    public void Decide(PlayerState state)
    {
        StartCoroutine(DecideCoroutine(state));
    }

    IEnumerator DecideCoroutine(PlayerState state)
    {
        yield return new WaitForSeconds(2);
        int cards = state.GetPlayerHand().CountCards();
        if (cards < 2)
        {
            state.GetDeck().DrawRandomCard();
            PlayerState.GetPlayerState(this).TurnOver();
            PlayerState.GetPlayerState(this).SignalToOpponent();
        }
        else
        {
            // 50/50 to play a random card or draw another card
            int random = UnityEngine.Random.Range(0, 2);
            if (random == 0)
            {
                state.GetPlayerHand().PlayRandomCard();
            }
            else
            {
                state.GetDeck().DrawRandomCard();
                PlayerState.GetPlayerState(this).TurnOver();
                PlayerState.GetPlayerState(this).SignalToOpponent();
            }
        }
    }
}