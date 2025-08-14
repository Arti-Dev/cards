using System;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Player player1 = null;
    [SerializeField] private Player player2 = null;
    [SerializeField] private Player opponent1 = null;
    [SerializeField] private Player opponent2 = null;

    [SerializeField] private CardDatabase cardDatabase = null;

    private int activePlayerIndex = 0;
    private List<Player> turnOrder = new List<Player>();
    

    void Start()
    {
        if (cardDatabase == null)
        {
            throw new SystemException("CardDatabase not initialized");
        }
        cardDatabase.InitDictionary();
        if (player1) player1.SetGame(this);
        if (player2) player2.SetGame(this);
        if (opponent1) opponent1.SetGame(this);
        if (opponent2) opponent2.SetGame(this);
        
        if (!player1 && !player2) 
        {
            throw new SystemException("There are no players!");
        }

        if (!opponent1 && !opponent2)
        {
            throw new SystemException("There are no opponents!");
        }
        
        // Build turn order
        if (player1) turnOrder.Add(player1);
        if (player2) turnOrder.Add(player2);
        if (opponent1) turnOrder.Add(opponent1);
        if (opponent2) turnOrder.Add(opponent2);
        
        turnOrder[0].TurnStart();
    }

    public void NextTurn(Player whoEndedTurn)
    {
        if (!whoEndedTurn) return;
        // Player who ended the turn must be the active player
        if (whoEndedTurn != turnOrder[activePlayerIndex]) return;
        
        activePlayerIndex++;
        if (activePlayerIndex >= turnOrder.Count) activePlayerIndex = 0;
        turnOrder[activePlayerIndex].TurnStart();
        Debug.Log(turnOrder[activePlayerIndex].name + "'s turn");
    }

    public Player GetPlayer1()
    {
        return player1;
    }
    
    public Player GetPlayer2()
    {
        return player2;
    }
    
    public Player GetCpuPlayer1()
    {
        return opponent1;
    }
    
    public Player GetCpuPlayer2()
    {
        return opponent2;
    }
}