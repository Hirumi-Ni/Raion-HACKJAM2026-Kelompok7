using System;

//kek nya ini terlalu di overuse deh, tapi gk tau la males refactor juga
public static class EventHandler
{
    //---===---===-[Action]-===---===---//
    //Animal Capture Events
    public static event Action<int> OnCaptureIncreaseObjective; //sheep
    public static event Action<int> OnCaptureDecreaseObjective; //wendigo klo gagal 

    public static event Action<float> OnCaptureDecreaseTrailResource; //wolf

    public static event Action<float> OnCaptureIncreasePlayerSpeed; //green sheep, nambah speed
    public static event Action<float> OnCaptureDecreasePlayerSpeed; //wendigo klo gagal

    public static event Action<float> OnCaptureGoldRush; //(durasi + ganti bool goldrush) sheep emas
    public static event Action<float> OnCaptureBlindPlayer; //(durasi) sheep hitam (kambing hitam awkoakw)
    
    //Game and Trail Events
    public static event Action OnObjectiveChanged;
    public static event Action<float> OnTrailResourceGain; //ngeheal
    public static event Action OnTimerEnded; //trigger timer selesai (timer 0)
    public static event Action<bool> OnGameEnded; //true menang, false kalah
    
    //---===---===-[Method]-===---===---//
    //Animal Capture Method
    public static void WhenCaptureIncreaseObjective(int amount) => OnCaptureIncreaseObjective?.Invoke(amount);
    public static void WhenCaptureDecreaseObjective(int amount) => OnCaptureDecreaseObjective?.Invoke(amount);
    public static void WhenCapturedDecreaseTrailResource(float amount) => OnCaptureDecreaseTrailResource?.Invoke(amount);
    public static void WhenCaptureIncreasePlayerSpeed(float amount) => OnCaptureIncreasePlayerSpeed?.Invoke(amount);
    public static void WhenCaptureDecreasePlayerSpeed(float amount) => OnCaptureDecreasePlayerSpeed?.Invoke(amount);
    public static void WhenCaptureGoldRush(float duration) => OnCaptureGoldRush?.Invoke(duration);
    public static void WhenCaptureBlindPlayer(float duration) => OnCaptureBlindPlayer?.Invoke(duration);

    //Game and Trail Method
    public static void WhenObjectiveChanged() => OnObjectiveChanged?.Invoke();
    public static void WhenTrailResourceGain(float amount) => OnTrailResourceGain?.Invoke(amount);
    public static void WhenTimerEnded() => OnTimerEnded?.Invoke();
    public static void WhenGameEnded(bool result) => OnGameEnded?.Invoke(result);
}