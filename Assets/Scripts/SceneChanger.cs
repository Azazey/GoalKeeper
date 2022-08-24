using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private int _sceneNumber;
    [SerializeField] private Button _button;

    private void ChangeScene()
    {
        SceneManager.LoadScene(_sceneNumber);
    }

    private void Start()
    {
        _button.onClick.AddListener(ChangeScene);
    }
}
