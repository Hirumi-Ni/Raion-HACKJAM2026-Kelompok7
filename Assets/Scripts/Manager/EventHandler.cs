using System;

public static class EventHandler
{
    public static event Action OnAnimalCaptured;
    public static event Action OnAnimalSacrificed;
    public static event Action OnTrailResourceGain;
    public static event Action<bool> OnGameEnded; //true game menang, false game kalah (game over)

    public static void WhenAnimalCaptured() => OnAnimalCaptured?.Invoke();
    public static void WhenAnimalSacrificed() => OnAnimalSacrificed?.Invoke();
    public static void WhenTrailResourceGain() => OnTrailResourceGain?.Invoke();
    public static void WhenGameEnded(bool result) => OnGameEnded?.Invoke(result);
}