using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDrag : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _body;
    [SerializeField] private DragListener _dragListener;
    [SerializeField] private Rigidbody[] _rigidbody;
    [SerializeField] private float _time;
    [SerializeField] private Animator _animator;

    private Ray _ray;
    private Vector3 _point;
    private float _distance;
    private bool _isDragged;

    public void StartRagdoll()
    {
        for (int i = 0; i < _rigidbody.Length; i++)
        {
            _rigidbody[i].isKinematic = false;
        }

        _animator.enabled = false;
    }

    public void StopRagdoll()
    {
        for (int i = 0; i < _rigidbody.Length; i++)
        {
            _rigidbody[i].isKinematic = true;
        }

        _animator.enabled = true;
    }

    public void SetAnimation(string animationFlag)
    {
        _animator.SetTrigger(animationFlag);
    }
    
    private void Start()
    {
        _dragListener.BeginTouch += DragListenerOnBeginTouch;
        _dragListener.Drag += DragListenerOnDrag;
        _dragListener.EndTouch += DragListenerOnEndTouch;
    }
    
    private void Update()
    {
        // Debug.DrawRay(_ray.origin, _ray.direction * _distance, Color.red);
        if (_isDragged)
            _body.position = Vector3.MoveTowards(_body.position, _point, Time.deltaTime * _time);
    }

    private void DragListenerOnBeginTouch(Vector2 position)
    {
        _isDragged = true;
    }

    private void DragListenerOnDrag(Vector2 position)
    {
        // Debug.Log(position);
        _ray = _camera.ScreenPointToRay(position);
        Plane plane = new Plane(-Vector3.forward, Vector3.zero);
        plane.Raycast(_ray, out _distance);
        _point = _ray.GetPoint(_distance);
        _point.y = Mathf.Clamp(_point.y, 0f, _point.y);
        // _rigidbody.isKinematic = true;
        // transform.position = Vector3.Lerp(transform.position, point, _speed * Time.deltaTime);
    }
    
    private void DragListenerOnEndTouch(Vector2 position)
    {
        _isDragged = false;
    }
}
