using System;
using UnityEngine;
using UnityEngine.Rendering;

public class GameResultManager : MonoBehaviour
{
    [SerializeField] private GameObject winUI;
    [SerializeField] private GameObject loseUI;
    [SerializeField] private TimerController timerController;
    [SerializeField] private CanvasGroup gameResultFadeObject; //panel, text, button restart + button main menu
    [SerializeField] private GameObject winLogo; 
    [SerializeField] private GameObject loseLogo; 

    private void OnEnable()
    {
        EventHandler.OnGameEnded += HandleGameEnd;
    }

    private void OnDisable()
    {
        EventHandler.OnGameEnded -= HandleGameEnd;
    }

    private void Start()
    {
        gameResultFadeObject.alpha = 0f;
        winUI.SetActive(false);
        loseUI.SetActive(false);
    }

    private void HandleGameEnd(bool result)
    {
        if (result) timerController.FinishLevel();
        Time.timeScale = 0;

        GameObject resultUI = result ? winUI : loseUI;
        resultUI.SetActive(true);

        InitializeAnimation(resultUI);
    }

    private void InitializeAnimation(GameObject resultUI)
    {
        UIAnimationManager.instance.Fade(gameResultFadeObject, 1);
        UIAnimationManager.instance.Stamp(resultUI.transform, resultUI.transform.localScale);
    }

    public void ButtonRestartLevel()
    {
        GameManager.instance.RestartScene();
    }    

    public void ButtonBackToMainMenu()
    {
        GameManager.instance.ChangeScene(GameScene.MainMenu);
    }

    public void ButtonBackToLevelSelect()
    {
        GameManager.instance.ChangeScene(GameScene.LevelSelection);
    }

    public void ChangeToEndCutscene()
    {
        GameManager.instance.ChangeScene(GameScene.EndCutscene);
    }
}
