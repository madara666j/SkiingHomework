using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public float timeElapsed = 0f;
    public bool timerRunning = true;
    public TextMeshProUGUI timerText;

    //  Add this line
    public TextMeshProUGUI penaltyText;

    private float penaltyDisplayTime = 1.5f; // How long "+1 second" shows
    private float penaltyTimer = 0f;

    void Update()
    {
        if (timerRunning)
        {
            timeElapsed += Time.deltaTime;
        }

        DisplayTime(timeElapsed);

        // Hide "+1 second" text after a moment
        if (penaltyTimer > 0)
        {
            penaltyTimer -= Time.deltaTime;
            if (penaltyTimer <= 0)
            {
                penaltyText.text = "";
            }
        }
    }

    public void AddPenalty(float seconds)
    {
        timeElapsed += seconds;
        Debug.Log("Penalty added: +" + seconds + "s");

        // Show "+1 second" on screen
        penaltyText.text = "+1 second!";
        penaltyTimer = penaltyDisplayTime;
    }

    void DisplayTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        int milliseconds = Mathf.FloorToInt((time * 1000) % 1000);

        timerText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }
}
