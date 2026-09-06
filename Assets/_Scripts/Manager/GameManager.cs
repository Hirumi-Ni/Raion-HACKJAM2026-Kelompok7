using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public enum GameScene
{
    MainMenu,
    StartingCutscene,
    LevelSelection,
    Level1,
    Level2,
    Level3,
    EndCutscene,
    Tutorial //paling
}

public class GameManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup transitionCanvas;
    [SerializeField] private int transitionDuration;
    private bool isChangingScene;

    public static GameManager instance;

    public string CurrentScene => SceneManager.GetActiveScene().name;
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
        if (isChangingScene) return;

        StartCoroutine(ChangeSceneRoutine(sceneEnum.ToString()));
    }

    public void RestartScene()
    {
        if (isChangingScene) return;

        StartCoroutine(ChangeSceneRoutine(SceneManager.GetActiveScene().name));
    }

    private IEnumerator ChangeSceneRoutine(string sceneName)
    {
        isChangingScene = true;
        yield return transitionCanvas.DOFade(1f, transitionDuration).SetUpdate(true).WaitForCompletion();

        Time.timeScale = 1f;
        yield return SceneManager.LoadSceneAsync(sceneName);
        if (AudioManager.instance != null) AudioManager.instance.UpdateBGM(CurrentScene);

        yield return transitionCanvas.DOFade(0f, transitionDuration).SetUpdate(true).WaitForCompletion();

        isChangingScene = false;
    }
}
