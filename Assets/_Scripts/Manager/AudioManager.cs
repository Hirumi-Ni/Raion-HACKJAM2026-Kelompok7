using Ami.BroAudio;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
    BGM1_Menu,
    BGM2_Gameplay,
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }

    [SerializeField] private SoundID soundBGM1;
    [SerializeField] private SoundID soundBGM2;

    private SoundType? currentBGM;

    private Dictionary<SoundType, SoundID> soundMappingDictionary;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        soundMappingDictionary = new Dictionary<SoundType, SoundID>
        {
            { SoundType.BGM1_Menu, soundBGM1 },
            { SoundType.BGM2_Gameplay, soundBGM2 },
        };
    }

    private void Start()
    {
        if (GameManager.instance != null)
        {
            UpdateBGM(GameManager.instance.CurrentScene);
        }
    }

    private void OnSceneLoad()
    {

    }

    public void UpdateBGM(string sceneName)
    {
        SoundType targetBGM;

        if (sceneName == GameScene.MainMenu.ToString() ||
            sceneName == GameScene.LevelSelection.ToString() || 
            sceneName == GameScene.StartingCutscene.ToString())
        {
            targetBGM = SoundType.BGM1_Menu;
        }
        else
        {
            targetBGM = SoundType.BGM2_Gameplay;
        }

        if (currentBGM.HasValue && currentBGM.Value == targetBGM) return;

        StopAllBGM();
        PlayAudio(targetBGM);

        currentBGM = targetBGM;
    }

    private void StopAllBGM()
    {
        StopAudio(SoundType.BGM1_Menu);
        StopAudio(SoundType.BGM2_Gameplay);
    }

    public void PlayAudio(SoundType sound)
    {
        if (soundMappingDictionary.TryGetValue(sound, out SoundID id))
        {
            BroAudio.Play(id);
        }
        else
        {
            Debug.LogWarning($"SoundType '{sound}' gk ada.");
        }
    }

    public void StopAudio(SoundType sound)
    {
        if (soundMappingDictionary.TryGetValue(sound, out SoundID id))
        {
            BroAudio.Stop(id);
        }
    }
}
