using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] protected Rigidbody _rigidbody;
    [SerializeField] protected float _force = 1;
    [SerializeField] private float _destroyTime = 5;

    public Action OnItemHit;

    protected bool _itemHit;
    protected Vector3 _directionToTarget;

    public void Throw(Transform target)
    {
        _itemHit = false;
        Vector3 toTarget = target.position - transform.position;
        _directionToTarget = toTarget;
        _rigidbody.AddForce(toTarget.normalized * _force, ForceMode.VelocityChange);
        Destroy(gameObject, _destroyTime);
    }
}