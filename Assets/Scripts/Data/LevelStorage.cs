using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStorage : MonoBehaviour
{
    public static LevelStorage Storage { get; private set; }

    public List<LevelSettings> Levels = new List<LevelSettings>();

    private const string _currentLevel = "currentLevel";

    public List<LevelSettings> GetLevelList()
    {
        if (Levels.Count == 0)
        {
            CreateLevelList();
        }

        return Levels;
    }

    public LevelSettings GetCurrentLevel()
    {
        if (!PlayerPrefs.HasKey(_currentLevel))
        {
            SetStartLevel();
        }

        return Levels.Find(item => item.name == PlayerPrefs.GetString(_currentLevel));
        ;
    }

    public void SetCurrentLevel(LevelSettings level)
    {
        PlayerPrefs.SetString(_currentLevel, level.name);
        PlayerPrefs.Save();
    }

    public void ResetAllLevels()
    {
        for (int i = 0; i < GetLevelList().Count; i++)
        {
            if (i == 0)
            {
                continue;
            }
            else
            {
                Levels[i].Bought = false;
                PlayerPrefs.SetInt(Levels[i].name, Convert.ToInt32(Levels[i].Bought));
            }
        }
    }

    private void SetStartLevel()
    {
        PlayerPrefs.SetString(_currentLevel, Levels[0].name);
        PlayerPrefs.Save();
    }

    private void CreateLevelList()
    {
        foreach (LevelSettings level in Resources.LoadAll<LevelSettings>("Levels"))
        {
            Levels.Add(level);
            if (!PlayerPrefs.HasKey(level.name))
            {
                PlayerPrefs.SetInt(level.name, Convert.ToInt32(level.Bought));
            }
            else
            {
                level.Bought = Convert.ToBoolean(PlayerPrefs.GetInt(level.name));
            }

            // Debug.Log("Objects was found: " + level.name);
        }
    }


    private void Awake()
    {
        if (Storage == null)
        {
            // Debug.Log("Storage was made");
            Storage = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            // Debug.Log("Founded another storage. Deleting unnecessary storage");
            Destroy(gameObject);
            return;
        }

        GetLevelList();
        SetStartLevel();
        // Debug.Log(GetCurrentLevel().name);
    }
}