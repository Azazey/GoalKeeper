using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Left,
    Right
}

public class BallThrower : MonoBehaviour
{
    [SerializeField] private Transform _leftTarget;
    [SerializeField] private Transform _rightTarget;
    [SerializeField] private float _speed;
    [SerializeField] private Direction _currentDirection;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _spawnPosition;
    // [SerializeField] private float _spawnTime;
    [SerializeField] private Transform _target;
    [SerializeField] private Item[] _items;

    private const string _triggerGoLeft = "GoLeft";

    private const string _triggerGoRight = "GoRight";

    public Item Shoot()
    {
        Item item;
        if (Random.Range(0, 100) < 60)
            item = (Ball) Instantiate(_items[0], _spawnPosition.position, _spawnPosition.rotation);
        
        else if (Random.Range(0, 100) < 50)
            item = (Bomb) Instantiate(_items[1], _spawnPosition.position, _spawnPosition.rotation);
        
        else
            item = (Coin) Instantiate(_items[2], _spawnPosition.position, _spawnPosition.rotation);
        item.Throw(_target);
        return item;
    }

    public Item[] ShootMany()
    {
        
        return null;
    }
    
    private void Awake()
    {
        _leftTarget.parent = null;
        _rightTarget.parent = null;

        SetAnimation();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (_currentDirection == Direction.Left)
        {
            transform.position -= new Vector3(Time.deltaTime * _speed, 0f, 0f);
            if (transform.position.x < _leftTarget.position.x)
            {
                _currentDirection = Direction.Right;
                SetAnimation();
            }
        }
        else
        {
            transform.position += new Vector3(Time.deltaTime * _speed, 0f, 0f);
            if (transform.position.x > _rightTarget.position.x)
            {
                _currentDirection = Direction.Left;
                SetAnimation();
            }
        }

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            transform.position = hit.point;
        }
    }

    private void SetAnimation()
    {
        switch (_currentDirection)
        {
            case Direction.Left:
                _animator.SetTrigger(_triggerGoLeft);
                break;
            case Direction.Right:
                _animator.SetTrigger(_triggerGoRight);
                break;
        }
    }
}