using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelStore : MonoBehaviour
{
    [SerializeField] private LevelButton _buttonPrefab;
    [SerializeField] private GameObject _spawnObject;
    [SerializeField] private GameObject _buyMenu;
    [SerializeField] private MoneyUI _moneyUi;
    [SerializeField] private TextMeshProUGUI _firstReq;
    [SerializeField] private TextMeshProUGUI _secondReq;
    [SerializeField] private Button _yesButton;

    private LevelSettings _currentLevelToBuy;
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
        if (level.Bought)
        {
            LevelStorage.Storage.SetCurrentLevel(level);
            Fader.FadeMaker.LoadScene(1);
        }
        else
        {
            _currentLevelToBuy = level;
            bool previousBought;
            bool enoughMoney;
            List<LevelSettings> levels = LevelStorage.Storage.GetLevelList();
            _buyMenu.SetActive(true);
            if (levels[levels.FindIndex(item => item.name == level.name) - 1].Bought)
            {
                _firstReq.color = Color.green;
                previousBought = true;
            }
            else
            {
                _firstReq.color = Color.red;
                previousBought = false;
            }
            
            _secondReq.text = "- Coins:" + level.Cost;
            if (PlayerPrefs.GetInt(PlayerBelongs.Money) >= level.Cost)
            {
                _secondReq.color = Color.green;
                enoughMoney = true;
            }
            else
            {
                _secondReq.color = Color.red;
                enoughMoney = false;
            }

            if (previousBought && enoughMoney)
            {
                _yesButton.interactable = true;
            }
            else
            {
                _yesButton.interactable = false;
            }
            
            _yesButton.onClick.RemoveAllListeners();
            _yesButton.onClick.AddListener(BuyLevel);
        }
    }

    private void BuyLevel()
    {
        _currentLevelToBuy.Bought = true;
        PlayerPrefs.SetInt(_currentLevelToBuy.name, Convert.ToInt32(_currentLevelToBuy.Bought));
        PlayerBelongs.OnLevelBuy(_currentLevelToBuy.Cost);
        _moneyUi.WriteMoney();
        _buyMenu.SetActive(false);
        foreach (var button in _buttons)
        {
            button.GetComponent<LevelButton>().CheckLockStatus();
        }
    }
}
