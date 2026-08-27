using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameScene
{
    MainMenu,
    LevelSelection,
    Level1,
    Level2,
    Level3,
    Tutorial //paling
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    public void ChangeScene(GameScene sceneEnum)
    {
        SceneManager.LoadScene(sceneEnum.ToString());
        Time.timeScale = 1f;
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }
}
