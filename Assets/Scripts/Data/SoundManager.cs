using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Manager { get; private set; }

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _menuMusic;
    [SerializeField] private AudioClip _gameMusic;

    public AudioSource AudioSource => _audioSource;

    public const string Volume = "Volume";

    public const string Music = "Music";


    private void SetMenuMusic()
    {
        _audioSource.Stop();
        _audioSource.clip = _menuMusic;
        _audioSource.Play();
    }

    public void StopMusic()
    {
        _audioSource.Stop();
    }

    public void SetGameMusic()
    {
        _audioSource.Stop();
        _audioSource.clip = _gameMusic;
        _audioSource.Play();
    }

    private void Awake()
    {
        if (Manager == null)
        {
            Manager = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
            SoundManager.Manager.SetMenuMusic();
            return;
        }

        CheckSettings();
        AudioListener.volume = PlayerPrefs.GetFloat(Volume);
        if (_audioSource)
        {
            _audioSource.enabled = Convert.ToBoolean(PlayerPrefs.GetInt(Music));
        }

        SetMenuMusic();
    }

    private void CheckSettings()
    {
        if (!PlayerPrefs.HasKey(Volume) && !PlayerPrefs.HasKey(Music))
        {
            SetStandartSettings();
        }
    }

    private void SetStandartSettings()
    {
        PlayerPrefs.SetFloat(Volume, 1);
        PlayerPrefs.SetInt(Music, 1);
    }
}