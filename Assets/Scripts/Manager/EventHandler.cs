using System;

public static class EventHandler
{
    public static event Action OnAnimalCaptured;
    public static event Action OnAnimalSacrificed;

    public static void WhenAnimalCaptured() => OnAnimalCaptured?.Invoke();
    public static void WhenAnimalSacrificed() => OnAnimalSacrificed?.Invoke();
}