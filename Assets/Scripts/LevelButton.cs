using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private Image _levelIcon;
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _lockImage;

    private LevelSettings _level;

    public event Action<LevelSettings> LevelChanged;

    public void Init(LevelSettings level)
    {
        _levelIcon.sprite = level.Sprite;
        _level = level;
        _button.onClick.AddListener(ChangeLevel);
        CheckLockStatus();
    }

    public void CheckLockStatus()
    {
        if (_level.Bought)
        {
            _lockImage.SetActive(false);
        }
        else
        {
            _lockImage.SetActive(true);
        }
    }

    private void ChangeLevel()
    {
        LevelChanged?.Invoke(_level);
    }
}