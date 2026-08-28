using UnityEngine;

public class LevelSelectScript : MonoBehaviour
{
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
