using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStore : MonoBehaviour
{
    [SerializeField] private LevelButton _buttonPrefab;
    [SerializeField] private GameObject _spawnObject;
    
    private List<GameObject> _buttons = new List<GameObject>();

    private void Start()
    {
        CreateSkinChangeButtons();
    }

    private void CreateSkinChangeButtons()
    {
        foreach (LevelSettings knifePropertiese in LevelStorage.Storage.GetLevelList())
        {
            LevelButton skinButton = Instantiate(_buttonPrefab, _spawnObject.transform);
            skinButton.Init(knifePropertiese);
            skinButton.LevelChanged += SelectLevel;
            _buttons.Add(skinButton.gameObject);
        }
    }
    
    private void SelectLevel(LevelSettings level)
    {
        
    }
}
