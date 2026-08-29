using TMPro;
using UnityEngine;

public class LevelSelectScript : MonoBehaviour
{
    [SerializeField] private TMP_Text level1BestTimeText;
    [SerializeField] private TMP_Text level2BestTimeText;
    [SerializeField] private TMP_Text level3BestTimeText;

    private void Start()
    {
        DisplayBestTimes();
    }

    private void DisplayBestTimes()
    {
        level1BestTimeText.text = HighscoreManager.GetBestTimeText(1);
        level2BestTimeText.text = HighscoreManager.GetBestTimeText(2);
        level3BestTimeText.text = HighscoreManager.GetBestTimeText(3);
    }

    public void ButtonReturnToMainMenu()
    {
        GameManager.instance.ChangeScene(GameScene.MainMenu);
    }

    public void ButtonLevel1()
    {
        GameManager.instance.ChangeScene(GameScene.Level1);
    }

    public void ButtonLevel2()
    {
        GameManager.instance.ChangeScene(GameScene.Level2);
    }
    public void ButtonLevel3()
    {
        GameManager.instance.ChangeScene(GameScene.Level3);
    }
}
