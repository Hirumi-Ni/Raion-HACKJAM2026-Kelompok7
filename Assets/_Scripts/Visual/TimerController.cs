using UnityEngine;

public class TimerController : MonoBehaviour
{
    [SerializeField] private float remainingTime;
    private bool timerEnded = false;

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

            EventHandler.WhenTimerEnded();
        }
    }

    public string GetTimerText()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
