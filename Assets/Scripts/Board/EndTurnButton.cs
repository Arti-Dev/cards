using UnityEngine;

namespace Board
{
    public class EndTurnButton : MonoBehaviour
    {
        private Player player;
        
        void Awake()
        {
            player = Player.GetPlayer(this);
        }
        void OnMouseDown()
        {
            if (!player.IsTurn() || !player.IsActionable())
            {
                Game.Log("You can't do this right now!");
                return;
            }
            player.TurnOver();
            gameObject.SetActive(false);
        }
    }
}