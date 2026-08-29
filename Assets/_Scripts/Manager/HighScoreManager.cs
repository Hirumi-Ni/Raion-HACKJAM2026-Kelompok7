using UnityEngine;

public static class HighscoreManager
{
    private static string HighscorePrefix = "LevelHighscore_";

    public static void SaveTime(int levelID, float completionTime)
    {
        Debug.Log($"Time Saved for {levelID} with highscore of {completionTime}");

        string key = GetKey(levelID);

        if (!PlayerPrefs.HasKey(key))
        {
            PlayerPrefs.SetFloat(key, completionTime);
            PlayerPrefs.Save();
            return;
        }

        float previousBest = PlayerPrefs.GetFloat(key);

        if (completionTime < previousBest)
        {
            PlayerPrefs.SetFloat(key, completionTime);
            PlayerPrefs.Save();
        }
    }

    public static float GetBestTime(int levelID)
    {
        string key = GetKey(levelID);

        if (!PlayerPrefs.HasKey(key)) return -1f;

        return PlayerPrefs.GetFloat(key);
    }

    public static string GetBestTimeText(int levelID)
    {
        float bestTime = GetBestTime(levelID);

        if (bestTime < 0f) return "--:--";

        int minutes = Mathf.FloorToInt(bestTime / 60f);
        int seconds = Mathf.FloorToInt(bestTime % 60f);

        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private static string GetKey(int levelID)
    {
        return HighscorePrefix + levelID;
    }

    public static void ResetAllScores()
    {
        for (int i = 1; i <= 3; i++)
        {
            PlayerPrefs.DeleteKey(GetKey(i));
        }

        PlayerPrefs.Save();
    }
}
