using UnityEngine;

public class SettingsScript : MonoBehaviour
{
    public void ButtonResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
