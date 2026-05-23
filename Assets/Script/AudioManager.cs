using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;

    public AudioSource sfxSource;

    private float musicVolume = 1f;

    private float sfxVolume = 1f;

    void Awake()
    {
        // singleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // load saved volume
        musicVolume =
            PlayerPrefs.GetFloat(
                "MusicVolume",
                1f
            );

        sfxVolume =
            PlayerPrefs.GetFloat(
                "SFXVolume",
                1f
            );

        ApplyVolume();
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;

        PlayerPrefs.SetFloat(
            "MusicVolume",
            volume
        );

        ApplyVolume();
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume;

        PlayerPrefs.SetFloat(
            "SFXVolume",
            volume
        );

        ApplyVolume();
    }

    void ApplyVolume()
    {
        if (musicSource != null)
        {
            musicSource.volume =
                musicVolume;
        }

        if (sfxSource != null)
        {
            sfxSource.volume =
                sfxVolume;
        }
    }

    public float GetMusicVolume()
    {
        return musicVolume;
    }

    public float GetSFXVolume()
    {
        return sfxVolume;
    }
}