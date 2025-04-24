using UnityEngine;

public class EndGate : MonoBehaviour
{
    public EndGameMenu endMenu;
    public GameTimer gameTimer;  // <-- Add this

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Stop the player
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.StopMovement();
            }

            // Stop the timer
            if (gameTimer != null)
            {
                gameTimer.timerRunning = false;
            }

            // Show end game menu
            if (endMenu != null)
            {
                endMenu.ShowMenu();
            }
        }
    }
}
