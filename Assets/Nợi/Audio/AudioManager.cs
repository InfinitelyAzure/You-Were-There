using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Sound Data")]
    public List<SoundData> sounds;

    [Header("Mixer")]
    public AudioMixer mixer;
    public string bgmVolumeParam = "BG";
    public string sfxVolumeParam = "SFX";

    [Header("Audio Sources")]
    public AudioSource bgmSource;
    public int sfxPoolSize = 10;

    // =======================
    Dictionary<SoundID, SoundData> soundDict;
    Dictionary<string, SoundID> idByName = new();
    List<AudioSource> sfxPool;

    const string PREF_BGM_VOL  = "audio_bgm_volume";
    const string PREF_SFX_VOL  = "audio_sfx_volume";
    const string PREF_BGM_MUTE = "audio_bgm_mute";
    const string PREF_SFX_MUTE = "audio_sfx_mute";

    // =======================
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        BuildSoundDictionary();
        InitSources();
        LoadSettings();
        
    }
    void Start()
    {
        Play("1 2");
    }

    // =======================
    void BuildSoundDictionary()
    {
        soundDict = new Dictionary<SoundID, SoundData>();
        idByName  = new Dictionary<string, SoundID>();

        foreach (var sound in sounds)
        {
            if (sound.soundID == null) continue;

            soundDict[sound.soundID] = sound;
            idByName[sound.soundID.name] = sound.soundID;
        }
    }
    

    void InitSources()
    {
        if (bgmSource == null)
            bgmSource = gameObject.AddComponent<AudioSource>();

        sfxPool = new List<AudioSource>();
        for (int i = 0; i < sfxPoolSize; i++)
        {
            var src = gameObject.AddComponent<AudioSource>();
            sfxPool.Add(src);
        }
    }

    // =======================
    #region PLAY API

    public static void Play(string soundName)
    {
        if (Instance == null) return;

        if (Instance.idByName.TryGetValue(soundName, out var id))
            Instance.PlayInternal(id);
        else
            Debug.LogWarning($"SoundID '{soundName}' not found");
    }


    void PlayInternal(SoundID id)
    {
        if (!soundDict.TryGetValue(id, out var sound)) return;

        switch (sound.type)
        {
            case SoundType.BGM:
                PlayBGM(sound);
                break;

            case SoundType.SFX:
                PlaySFX(sound);
                break;
        }
    }

    void PlayBGM(SoundData sound)
    {
        if (bgmSource.clip == sound.clip) return;

        bgmSource.Stop();
        bgmSource.clip   = sound.clip;
        bgmSource.loop   = sound.loop;
        bgmSource.volume = sound.volume;
        bgmSource.Play();
    }

    void PlaySFX(SoundData sound)
    {
        foreach (var src in sfxPool)
        {
            if (!src.isPlaying)
            {
                src.clip   = sound.clip;
                src.loop   = sound.loop;
                src.volume = sound.volume;
                src.Play();
                return;
            }
        }
    }

    #endregion

    // =======================
    #region VOLUME & MUTE

    public static void SetBGMVolume(float value)
    {
        Instance.mixer.SetFloat(Instance.bgmVolumeParam, ToDb(value));
        PlayerPrefs.SetFloat(PREF_BGM_VOL, value);
    }

    public static void SetSFXVolume(float value)
    {
        Instance.mixer.SetFloat(Instance.sfxVolumeParam, ToDb(value));
        PlayerPrefs.SetFloat(PREF_SFX_VOL, value);
    }

    public static void SetBGMMute(bool mute)
    {
        Instance.mixer.SetFloat(
            Instance.bgmVolumeParam,
            mute ? -80f : ToDb(GetBGMVolume())
        );
        PlayerPrefs.SetInt(PREF_BGM_MUTE, mute ? 1 : 0);
    }

    public static void SetSFXMute(bool mute)
    {
        Instance.mixer.SetFloat(
            Instance.sfxVolumeParam,
            mute ? -80f : ToDb(GetSFXVolume())
        );
        PlayerPrefs.SetInt(PREF_SFX_MUTE, mute ? 1 : 0);
    }

    static float GetBGMVolume()
        => PlayerPrefs.GetFloat(PREF_BGM_VOL, 1f);

    static float GetSFXVolume()
        => PlayerPrefs.GetFloat(PREF_SFX_VOL, 1f);

    static float ToDb(float value)
    {
        return value <= 0.0001f ? -80f : Mathf.Log10(value) * 20f;
    }

    #endregion

    // =======================
    void LoadSettings()
    {
        SetBGMVolume(GetBGMVolume());
        SetSFXVolume(GetSFXVolume());

        SetBGMMute(PlayerPrefs.GetInt(PREF_BGM_MUTE, 0) == 1);
        SetSFXMute(PlayerPrefs.GetInt(PREF_SFX_MUTE, 0) == 1);
    }
}
