using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;

public class EndGameMenu : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject resultsPanel;
    public LeaderboardManager leaderboardManager;
    public GameTimer gameTimer; // Reference to timer
    public TextMeshProUGUI leaderboardText; // UI Text to show top times

    // Call this when the player reaches the finish line
    public void ShowMenu()
    {
        // Show the results panel
        if (resultsPanel != null)
        {
            resultsPanel.SetActive(true);
        }

        // Save the current time to leaderboard
        if (leaderboardManager != null && gameTimer != null)
        {
            leaderboardManager.SaveNewTime(gameTimer.timeElapsed);

            // Show top 3 leaderboard times
            List<float> topTimes = leaderboardManager.GetTopTimes();
            leaderboardText.text = "Top 3 Times:\n";
            for (int i = 0; i < topTimes.Count; i++)
            {
                leaderboardText.text += $"{i + 1}. {FormatTime(topTimes[i])}\n";
            }
        }
    }

    public void RetryLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game Closed"); // Works only in Editor
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        int milliseconds = Mathf.FloorToInt((time * 1000) % 1000);
        return string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }
}
