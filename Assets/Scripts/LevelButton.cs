using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private Image _levelIcon;
    [SerializeField] private Button _button;

    private LevelSettings _level;

    public LevelSettings Level => _level;

    public event Action<LevelSettings> LevelChanged;

    public void Init(LevelSettings level)
    {
        _levelIcon.sprite = level.Sprite;
        _level = level;
        _button.onClick.AddListener(ChangeLevel);
    }

    private void ChangeLevel()
    {
        // KnifeStorage.Storage.SetCurrentKnife(_knifeProperties);
        LevelChanged?.Invoke(_level);
    }
}
