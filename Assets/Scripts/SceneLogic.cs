using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

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
    [SerializeField] private DragListener _dragListener;
    [SerializeField] private PlayerDrag _player;
    [SerializeField] private GameObject _uiStartText;
    [SerializeField] private AudioSource _whistle;

    private float _roundTime;
    private int _ballsProgress;
    private int _ballsNeedToPassLevel;
    private int _itemsFireRate;
    private int _maxItemsPerShoot;
    private bool _levelEnded;

    private Vector3 _cameraOriginPos = new Vector3(-0.12f, 5.48f, -8.6f);
    private Vector3 _cameraCutScenePos = new Vector3(-0.08f, 3.48f, 4.4f);

    public event Action OnTimeUpdate;

    private void ActivateMenu(GameObject menuToOpen, float delay)
    {
        for (int i = 0; i < _componentsToDisable.Length; i++)
        {
            _componentsToDisable[i].enabled = false;
        }

        HideObjects();
        SoundManager.Manager.StopMusic();
        _whistle.Play();

        StartCoroutine(MenuDelay(menuToOpen, delay));
    }

    private void HideObjects()
    {
        foreach (GameObject oneObject in _objectsToHide)
        {
            oneObject.SetActive(false);
        }
    }

    private void ShowObjects()
    {
        foreach (GameObject oneOjbect in _objectsToHide)
        {
            oneOjbect.SetActive(true);
        }
    }

    public float GetRoundTime() => _roundTime;

    private IEnumerator MenuDelay(GameObject prefab, float timeDelay)
    {
        WaitForSeconds delay = new WaitForSeconds(timeDelay);
        yield return delay;
        prefab.SetActive(true);
        StartCoroutine(ChangeCameraPosition(_cameraCutScenePos, _cameraOriginPos, new Vector3(15f, 180f, 0f),
            new Vector3(15f, 0f, 0f)));
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
        _ballsProgress = PlayerPrefs.GetInt(PlayerBelongs.Score);
        if (_ballsProgress >= _ballsNeedToPassLevel)
        {
            ActivateMenu(_winMenu, _winMenuTimeDelay);
            _levelEnded = true;
        }
        else
        {
            ActivateMenu(_looseMenu, _looseMenuTimeDelay);
            _levelEnded = true;
        }
    }

    private void SpawnItem()
    {
        if (_maxItemsPerShoot <= 1)
        {
            StartCoroutine(SpawnManyItems(1));
        }
        else
        {
            int itemsCount = Random.Range(1, _maxItemsPerShoot);
            StartCoroutine(SpawnManyItems(itemsCount));
        }
    }

    private IEnumerator SpawnManyItems(int itemsCount)
    {
        WaitForSeconds delay = new WaitForSeconds(1);
        WaitForSeconds repeat = new WaitForSeconds(_itemsFireRate);
        if (!_levelEnded)
        {
            for (int i = 0; i < itemsCount; i++)
            {
                Item item = _ballThrower.Shoot();
                for (int j = 0; j < _uiObjects.Length; j++)
                {
                    _uiObjects[j].AttachToAction(item);
                }

                yield return delay;
            }
        }

        yield return repeat;
        SpawnItem();
    }

    private IEnumerator ChangeCameraPosition(Vector3 pos1, Vector3 pos2, Vector3 rot1, Vector3 rot2)
    {
        Camera main = Camera.main;
        for (float t = 0.1f; t > 0f; t -= Time.deltaTime)
        {
            main.transform.position = Vector3.Lerp(pos1, pos2, t);
            main.transform.rotation = Quaternion.Euler(Vector3.Lerp(rot1, rot2, t));
            yield return null;
        }

        main.transform.position = pos1;
        main.transform.rotation = Quaternion.Euler(rot1);
    }

    private void Awake()
    {
        AudioListener.volume = 0f;
        Camera.main.transform.position = _cameraCutScenePos;
        _player.StopRagdoll();
        _player.SetAnimation("Sleepy");
        HideObjects();
        StartCutScene();
        _uiStartText.SetActive(true);
    }

    private void StartCutScene()
    {
        _dragListener.Touch += LevelInnit;
    }

    private void LevelInnit()
    {
        AudioListener.volume = 1f;
        SoundManager.Manager.SetGameMusic();
        _uiStartText.SetActive(false);
        _player.StartRagdoll();
        ShowObjects();
        StartCoroutine(ChangeCameraPosition(_cameraOriginPos, _cameraCutScenePos, new Vector3(15f, 0f, 0f),
            new Vector3(15f, 180f, 0f)));
        _dragListener.Touch -= LevelInnit;
        _levelEnded = false;
        LevelSettings currentLevel = LevelStorage.Storage.GetCurrentLevel();
        _looseMenu.SetActive(false);
        _winMenu.SetActive(false);
        _itemsFireRate = currentLevel.ItemsFireRate;
        _maxItemsPerShoot = currentLevel.MaxItemsPerFire;
        _ballsProgress = 0;
        _ballsNeedToPassLevel = Mathf.RoundToInt(0.9f * LevelStorage.Storage.GetCurrentLevel().MaxProgress);
        _roundTime = currentLevel.RoundTimeInSeconds;
        StartCoroutine(StartTimer());
        PlayerBelongs.OnGameStart();
        SpawnItem();
    }
}