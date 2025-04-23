using UnityEngine;

public class EndGate : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Stop the game timer
            GameTimer gameTimer = FindObjectOfType<GameTimer>();
            if (gameTimer != null)
            {
                gameTimer.timerRunning = false;
            }

            // Stop the player's movement
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.StopMovement();
            }

            Debug.Log("Finish line reached! Timer and player stopped.");
        }
    }
}
