using UnityEngine;

public class TimerController : MonoBehaviour
{
    [SerializeField] private float remainingTime;

    private void Update()
    {
        CalculateTimer();
    }

    private void CalculateTimer()
    {
        remainingTime -= Time.deltaTime;
    }

    public string GetTimerText()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
