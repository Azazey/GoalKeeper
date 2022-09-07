using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Toggle _toggle;

    public void ResetMoney()
    {
        PlayerBelongs.ResetMoney();
    }

    public void ResetAll()
    {
        PlayerBelongs.ResetAllBelongs();
    }
    
    public void SetMusicEnabled(bool value)
    {
        if (SoundManager.Manager.AudioSource)
        {
            SoundManager.Manager.AudioSource.enabled = value;
        }
        PlayerPrefs.SetInt(SoundManager.Music, Convert.ToInt32(value));
    }

    public void SetVolume(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat(SoundManager.Volume, value);
    }

    public void Awake()
    {
        _toggle.isOn = Convert.ToBoolean(PlayerPrefs.GetInt(SoundManager.Music));
        _slider.value = PlayerPrefs.GetFloat(SoundManager.Volume);
        QualitySettings.vSyncCount = 0;
        QualitySettings.shadows = ShadowQuality.Disable;
        QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
        QualitySettings.antiAliasing = 0;
        QualitySettings.shadowCascades = 0;
        Application.targetFrameRate = 1000;
    }
}
