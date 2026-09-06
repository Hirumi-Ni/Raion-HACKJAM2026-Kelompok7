using UnityEngine;

public class TimerController : MonoBehaviour
{
    [SerializeField] private float remainingTime = 60f;
    [SerializeField] private int levelID = 1;

    private float startingTime;
    private bool timerEnded = false;

    private void Start()
    {
        startingTime = remainingTime;
    }

    private void Update()
    {
        CalculateTimer();
    }

    private void CalculateTimer()
    {
        if (timerEnded) return;

        remainingTime -= Time.deltaTime;

        if (remainingTime <= 0f)
        {
            remainingTime = 0f;
            timerEnded = true;

            EventHandler.WhenGameEnded(false);
        }
    }

    public void FinishLevel()
    {
        if (timerEnded) return;
        timerEnded = true;

        float completionTime = startingTime - remainingTime;
        HighscoreManager.SaveTime(levelID, completionTime);
    }

    public string GetTimerText()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60f);
        int seconds = Mathf.FloorToInt(remainingTime % 60f);

        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public float GetCompletionTime()
    {
        return startingTime - remainingTime;
    }
}
