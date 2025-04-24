using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class LeaderboardManager : MonoBehaviour
{
    private const int MaxScores = 3;
    private const string LeaderboardKey = "LeaderboardTimes";

    public List<float> GetTopTimes()
    {
        string saved = PlayerPrefs.GetString(LeaderboardKey, "");
        List<float> times = saved.Split(',').Where(s => !string.IsNullOrEmpty(s)).Select(s => float.Parse(s)).ToList();
        return times.OrderBy(t => t).Take(MaxScores).ToList();
    }

    public void SaveNewTime(float newTime)
    {
        List<float> times = GetTopTimes();
        times.Add(newTime);
        times = times.OrderBy(t => t).Take(MaxScores).ToList();

        string saved = string.Join(",", times.Select(t => t.ToString()));
        PlayerPrefs.SetString(LeaderboardKey, saved);
        PlayerPrefs.Save();
    }

    public void ClearLeaderboard()
    {
        PlayerPrefs.DeleteKey(LeaderboardKey);
    }
}
