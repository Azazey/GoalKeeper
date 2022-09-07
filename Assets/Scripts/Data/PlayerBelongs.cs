using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class PlayerBelongs
{
    public const string Money = "Money";
    public const string Score = "Score";
    private const int _resetValue = 0;

    public static void AddScore(int value)
    {
        int maxScore = LevelStorage.Storage.GetCurrentLevel().MaxProgress;
        int currentScore = PlayerPrefs.GetInt(Score);
        if (currentScore < maxScore)
            currentScore += value;
        // if (currentScore >= maxScore)
        //     currentScore = maxScore;
        PlayerPrefs.SetInt(Score, Mathf.Clamp(currentScore, 0, maxScore));
    }

    public static void SubtractScore(int value)
    {
        int currentScore = PlayerPrefs.GetInt(Score);
        currentScore -= value;
        if (currentScore < 0)
            currentScore = 0;
        PlayerPrefs.SetInt(Score, currentScore);
    }

    public static void AddMoney(int value)
    {
        int money = PlayerPrefs.GetInt(Money);
        money += value;
        PlayerPrefs.SetInt(Money, money);
    }

    public static void OnLevelBuy(int moneyCost)
    {
        int money = PlayerPrefs.GetInt(Money);
        if (moneyCost <= money)
        {
            money -= moneyCost;
        }

        PlayerPrefs.SetInt(Money, money);
    }

    public static void ResetAllBelongs()
    {
        PlayerPrefs.SetInt(Score, _resetValue);
        PlayerPrefs.SetInt(Money, 2000);
        LevelStorage.Storage.ResetAllLevels();
        Fader.FadeMaker.LoadScene(0);
    }

    public static void ResetMoney()
    {
        PlayerPrefs.SetInt(Money, _resetValue);
    }

    public static void OnGameStart()
    {
        PlayerPrefs.SetInt(Score, _resetValue);
    }
}