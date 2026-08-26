using System;

//kek nya ini terlalu di overuse deh, tapi gk tau la males refactor juga
public static class EventHandler
{
    //---===---===-[Action]-===---===---//
    //Animal Capture Events
    public static event Action OnAnimalCaptured;
    public static event Action OnAnimalSacrificed;
    public static event Action<float> OnWolfCaptured;
    
    //Game and Trail Events
    public static event Action OnObjectiveChanged;
    public static event Action<float> OnTrailResourceGain;
    public static event Action OnTimerEnded; //trigger timer selesai (timer 0)
    public static event Action<bool> OnGameEnded; //true menang, false kalah
    
    //---===---===-[Method]-===---===---//
    //Animal Capture Method
    public static void WhenAnimalCaptured() => OnAnimalCaptured?.Invoke();
    public static void WhenAnimalSacrificed() => OnAnimalSacrificed?.Invoke();
    public static void WhenWolfCaptured(float amount) => OnWolfCaptured?.Invoke(amount);

    //Game and Trail Method
    public static void WhenObjectiveChanged() => OnObjectiveChanged?.Invoke();
    public static void WhenTrailResourceGain(float amount) => OnTrailResourceGain?.Invoke(amount);
    public static void WhenTimerEnded() => OnTimerEnded?.Invoke();
    public static void WhenGameEnded(bool result) => OnGameEnded?.Invoke(result);
}