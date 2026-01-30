using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public List<SoundData> sounds;

    Dictionary<SoundID, SoundData> soundDict;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        soundDict = new Dictionary<SoundID, SoundData>();

        foreach (var sound in sounds)
        {
            if (!soundDict.ContainsKey(sound.soundID))
                soundDict.Add(sound.soundID, sound);
        }
    }

    public static void Play(SoundID id)
    {
        if (Instance == null) return;
        Instance.PlayInternal(id);
    }

    void PlayInternal(SoundID id)
    {
        if (!soundDict.TryGetValue(id, out var sound)) return;

        Debug.Log($"Play sound: {id.name}");
        // xử lý BGM / SFX giống hệ trước
    }
}
