using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(menuName = "Audio/Sound Data")]
public class SoundData : ScriptableObject
{
    public SoundID soundID;
    public SoundType type;
    public AudioClip clip;
    public AudioMixer audioMixer;

    [Range(0f, 1f)] public float volume = 1f;
    [Range(0.5f, 2f)] public float pitch = 1f;
    public bool loop;
}
