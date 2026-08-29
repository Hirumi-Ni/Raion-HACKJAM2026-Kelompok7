using UnityEngine;

public class PauseScript : MonoBehaviour
{
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject pauseMenu;

    public void ButtonResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void ButtonRestartGame()
    {
        GameManager.instance.RestartScene();
    }

    public void ButtonMainMenu()
    {
        GameManager.instance.ChangeScene(GameScene.MainMenu);
    }

    public void ButtonPauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }
}
