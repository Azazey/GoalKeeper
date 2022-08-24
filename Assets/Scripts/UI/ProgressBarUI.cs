using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : BaseUI
{
    [SerializeField] private Image _fill;
    
    private int _value;
    private int _maxValue;

    public override void AttachToAction(Item item)
    {
        item.OnItemHit += UpdateProgress;
    }
    
    private void UpdateProgress()
    {
        _value = PlayerPrefs.GetInt(PlayerBelongs.Score);
        _fill.fillAmount = (float) _value / _maxValue;
        Debug.Log(PlayerPrefs.GetInt(PlayerBelongs.Score));
    }

    private void Start()
    {
        UpdateProgress();
        _maxValue = LevelStorage.Storage.GetCurrentLevel().MaxProgress;
        _value = 0;
    }
}
