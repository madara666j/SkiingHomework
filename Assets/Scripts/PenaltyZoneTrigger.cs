using UnityEngine;

public class PenaltyZoneTrigger : MonoBehaviour
{
    private GameTimer gameTimer;

    private void Start()
    {
        gameTimer = FindObjectOfType<GameTimer>(); // Get the GameTimer component from the scene
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure the player is entering
        {
            Debug.Log("Entered penalty zone!"); // This will show up in the console
            gameTimer.AddPenalty(1f); // Add 1 second to the timer
        }
    }

}
