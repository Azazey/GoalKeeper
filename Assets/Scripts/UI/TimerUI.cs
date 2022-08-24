using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    [SerializeField] private SceneLogic _sceneLogic;
    [SerializeField] private TextMeshProUGUI _timer;
    
    private void UpdateTimeText()
    {
        float timeLeft = _sceneLogic.GetRoundTime();

        if (timeLeft < 0)
            timeLeft = 0;
            
        
        float minutes = Mathf.FloorToInt(timeLeft / 60);
        float seconds = Mathf.FloorToInt(timeLeft % 60);
        _timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void Start()
    {
        _sceneLogic.OnTimeUpdate += UpdateTimeText;
    }
}
