using UnityEngine;
using Ami.BroAudio;
using System.Collections.Generic;

public enum SoundType
{
    BGM,
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }

    [SerializeField] private SoundID soundBGM;

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
            { SoundType.BGM, soundBGM },
        };
    }

    private void Start()
    {
        PlayAudio(SoundType.BGM);
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