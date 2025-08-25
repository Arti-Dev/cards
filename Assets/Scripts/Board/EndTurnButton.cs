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
                Debug.Log("It's not your turn!");
                return;
            }
            player.TurnOver();
            gameObject.SetActive(false);
        }
    }
}