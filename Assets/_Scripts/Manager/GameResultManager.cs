using UnityEngine;

public class GameResultManager : MonoBehaviour
{
    [SerializeField] private GameObject winUI;
    [SerializeField] private GameObject loseUI;

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
        GameObject resultUI = result ? winUI : loseUI;
        resultUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void ButtonRestartLevel()
    {
        GameManager.instance.RestartScene();
    }    

    public void ButtonBackToMainMenu()
    {
        GameManager.instance.ChangeScene(GameScene.MainMenu);
    }
}
