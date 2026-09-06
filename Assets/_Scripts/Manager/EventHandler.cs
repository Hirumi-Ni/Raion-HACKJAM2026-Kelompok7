using System;

//kek nya ini terlalu di overuse deh, tapi gk tau la males refactor juga
public static class EventHandler
{
    //---===---===-[Action]-===---===---//
    //Animal Capture Events / Attribute Modifier
    public static event Action<int> OnIncreaseObjective; //sheep
    public static event Action<int> OnDecreaseObjective; //wendigo klo gagal 

    public static event Action<float> OnIncreaseTrailResource; //ngeheal
    public static event Action<float> OnDecreaseTrailResource; //wolf

    public static event Action<float, float> OnChangePlayerSpeed; //green sheep, nambah speed, atau slow speed

    public static event Action<float> OnGoldRush; //(durasi + ganti bool goldrush) sheep emas
    public static event Action<float> OnBlindPlayer; //(durasi) sheep hitam (kambing hitam awkoakw)
    
    //Game and Trail Events
    public static event Action OnObjectiveChanged;
    public static event Action<bool> OnGameEnded; //true menang, false kalah
    public static event Action<float, int> OnLevelCompleted;

    //---===---===-[Method]-===---===---//
    //Animal Capture Method
    public static void WhenIncreaseObjective(int amount) => OnIncreaseObjective?.Invoke(amount);
    public static void WhenDecreaseObjective(int amount) => OnDecreaseObjective?.Invoke(amount);

    public static void WhenIncreaseTrailResource(float amount) => OnIncreaseTrailResource?.Invoke(amount);
    public static void WhenDecreaseTrailResource(float amount) => OnDecreaseTrailResource?.Invoke(amount);

    public static void WhenChangePlayerSpeed(float amount, float duration) => OnChangePlayerSpeed?.Invoke(amount, duration);

    public static void WhenGoldRush(float duration) => OnGoldRush?.Invoke(duration);
    public static void WhenBlindPlayer(float duration) => OnBlindPlayer?.Invoke(duration);

    //Game and Trail Method
    public static void WhenObjectiveChanged() => OnObjectiveChanged?.Invoke();
    public static void WhenGameEnded(bool result) => OnGameEnded?.Invoke(result);
    public static void WhenLevelCompleted(float completionTime, int levelID) => OnLevelCompleted?.Invoke(completionTime, levelID);
}