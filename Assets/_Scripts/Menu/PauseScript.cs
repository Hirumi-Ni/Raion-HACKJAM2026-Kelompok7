using System.Runtime.CompilerServices;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private CanvasGroup[] pauseMenuButtonFades;
    [SerializeField] private GameObject pauseGameTitle;

    private void InitializeAnimation()
    {
        UIAnimationManager.instance.Stamp(pauseGameTitle.transform, pauseGameTitle.transform.localScale);
        for (int i = 0; i < pauseMenuButtonFades.Length; i++)
        {
            pauseMenuButtonFades[i].alpha = 0;
            UIAnimationManager.instance.Fade(pauseMenuButtonFades[i], 1);
        }
    }

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

    public void ButtonLevelSelect()
    {
        GameManager.instance.ChangeScene(GameScene.LevelSelection);
    }

    public void ButtonPauseGame()
    {
        InitializeAnimation();
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }
}
