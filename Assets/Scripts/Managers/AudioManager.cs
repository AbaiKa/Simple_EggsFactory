using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class AudioClipInfo
{
    public string id;
    public AudioClip clip;
}

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Audio Clips")]
    [SerializeField] private AudioClipInfo[] audioClips;

    private Dictionary<string, AudioClip> clipDict;

    private SettingsPanel settingsPanel;
    public void Init()
    {
        settingsPanel = FindFirstObjectByType<SettingsPanel>();
        settingsPanel.onSoundChanged.AddListener(SetSoundVolume);
        settingsPanel.onMusicChanged.AddListener(SetMusicVolume);

        clipDict = new Dictionary<string, AudioClip>();
        foreach (var info in audioClips)
        {
            if (!clipDict.ContainsKey(info.id) && info.clip != null)
                clipDict.Add(info.id, info.clip);
        }

        PlayMusic("game_music");
    }
    public void SetMusicVolume(float volume)
    {
        musicSource.volume = Mathf.Clamp01(volume);
    }
    public void SetSoundVolume(float volume)
    {
        sfxSource.volume = Mathf.Clamp01(volume);
    }
    public void PlaySound(string id)
    {
        if (clipDict.TryGetValue(id, out AudioClip clip))
        {
            sfxSource.PlayOneShot(clip);
        }
    }
    public void PlayMusic(string id)
    {
        if (clipDict.TryGetValue(id, out AudioClip clip))
        {
            musicSource.clip = clip;
            musicSource.loop = true;
            musicSource.Play();
        }
    }
}