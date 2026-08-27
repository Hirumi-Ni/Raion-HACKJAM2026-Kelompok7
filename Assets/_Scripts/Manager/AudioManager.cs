using UnityEngine;
using Ami.BroAudio;
using System.Collections.Generic;

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
        PlayAudio(SoundType.BGM1_Menu);
    }

    public void PlayAudio(SoundType sound)
    {
        if (soundMappingDictionary.TryGetValue(sound, out SoundID id)) BroAudio.Play(id);
        else Debug.Log($"Enum SoundType '{sound}' gk ada");
    }

    public void StopAudio(SoundType sound)
    {
        if (soundMappingDictionary.TryGetValue(sound, out SoundID id)) BroAudio.Stop(id);
    }
}