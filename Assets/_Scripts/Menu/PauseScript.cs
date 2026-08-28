using UnityEngine;

public class PauseScript : MonoBehaviour
{
    [SerializeField] private GameObject settingsMenu;

    public void ButtonRestartGame()
    {
        GameManager.instance.RestartScene();
    }

    public void ButtonMainMenu()
    {
        GameManager.instance.ChangeScene(GameScene.MainMenu);
    }

}
