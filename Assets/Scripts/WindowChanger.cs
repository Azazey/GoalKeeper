using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowChanger : MonoBehaviour
{
    [SerializeField] private GameObject _window;
    [SerializeField] private Button _button;
    [SerializeField] private bool _state;
    
    private void Start()
    {
        _button.onClick.AddListener(ChangeWindow);
    }

    private void ChangeWindow()
    {
        Fader.FadeMaker.LoadScreen(_window, _state);
    }
}
