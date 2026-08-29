using System;
using UnityEngine;

public class GameResultManager : MonoBehaviour
{
    [SerializeField] private GameObject winUI;
    [SerializeField] private GameObject loseUI;
    [SerializeField] private TimerController timerController;
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
        winUI.SetActive(false);
        loseUI.SetActive(false);
    }

    private void HandleGameEnd(bool result)
    {
        if (result) timerController.FinishLevel();
        Time.timeScale = 0;
        GameObject resultUI = result ? winUI : loseUI;
        resultUI.SetActive(true);
    }

    public void ButtonRestartLevel()
    {
        GameManager.instance.RestartScene();
    }    

    public void ButtonBackToMainMenu()
    {
        GameManager.instance.ChangeScene(GameScene.MainMenu);
    }

    public void ChangeToEndCutscene()
    {
        GameManager.instance.ChangeScene(GameScene.EndCutscene);
    }
}
