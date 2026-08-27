using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    public void StartGame()
    {
        GameManager.instance.ChangeScene(GameScene.LevelSelection);
    }

    public void ExitGame()
    {
    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
    #else
                Application.Quit();
    #endif
    }
}
