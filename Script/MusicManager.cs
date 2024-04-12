using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }
    private AudioSource musicSource;
    private float volume = 0.3f;
    private const string PLAYER_PREFS_MUSIC_VOLUME = "PlayerPrefsMusicVolume";

    private void Awake()
    {

        Instance = this;
        musicSource = GetComponent<AudioSource>();
        volume=PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC_VOLUME,0.3f);
        musicSource.volume = volume;

    }
    
    public void InscreaseMusicVolume()
    {
        volume += .1f;
        if(volume>1f)
        {
            volume = 0f;
        }
        musicSource.volume = volume;
        PlayerPrefs.SetFloat(PLAYER_PREFS_MUSIC_VOLUME, volume);
        PlayerPrefs.Save();
    }
    public float GetVolume() {
    return volume;
    }
}
