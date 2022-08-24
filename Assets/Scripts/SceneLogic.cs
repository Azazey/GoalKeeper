using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLogic : MonoBehaviour
{
    [SerializeField] private MonoBehaviour[] _componentsToDisable;
    [SerializeField] private GameObject[] _objectsToHide;
    [SerializeField] private GameObject _winMenu;
    [SerializeField] private GameObject _looseMenu;
    [SerializeField] private float _looseMenuTimeDelay;
    [SerializeField] private float _winMenuTimeDelay;
    [SerializeField] private BallThrower _ballThrower;
    [SerializeField] private BaseUI[] _uiObjects;

    private float _roundTime;
    private int _ballsProgress;
    private int _ballsNeedToPassLevel;

    public event Action OnTimeUpdate;

    public void ActivateMenu(GameObject menuToOpen, float delay)
    {
        for (int i = 0; i < _componentsToDisable.Length; i++)
        {
            _componentsToDisable[i].enabled = false;
        }

        foreach (GameObject oneObject in _objectsToHide)
        {
            oneObject.SetActive(false);
        }

        StartCoroutine(MenuDelay(menuToOpen, delay));
    }

    public float GetRoundTime() => _roundTime;

    private IEnumerator MenuDelay(GameObject prefab, float timeDelay)
    {
        WaitForSeconds delay = new WaitForSeconds(timeDelay);
        yield return delay;
        prefab.SetActive(true);
    }
    
    private IEnumerator StartTimer()
    {
        while (_roundTime > 0)
        {
            _roundTime -= Time.deltaTime;
            OnTimeUpdate?.Invoke();
            yield return null;
        }
        CheckLevelState();
    }

    private void CheckLevelState()
    {
        if (_ballsProgress >= _ballsNeedToPassLevel)
        {
            ActivateMenu(_winMenu, _winMenuTimeDelay);
        }
        else
        {
            ActivateMenu(_looseMenu, _looseMenuTimeDelay);
        }
    }

    private void SpawnItem()
    {
        Item item = _ballThrower.Shoot();
        for (int i = 0; i < _uiObjects.Length; i++)
        {
            _uiObjects[i].AttachToAction(item);
        }
    }

    private void Awake()
    {
        _looseMenu.SetActive(false);
        _winMenu.SetActive(false);
        _ballsProgress = 0;
        _ballsNeedToPassLevel = Mathf.RoundToInt(0.9f * LevelStorage.Storage.GetCurrentLevel().MaxProgress);
        _roundTime = LevelStorage.Storage.GetCurrentLevel().RoundTimeInSeconds;
        StartCoroutine(StartTimer());
        PlayerBelongs.OnGameStart();
        // Invoke("SpawnItem", 1);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SpawnItem();
        }
    }
}
