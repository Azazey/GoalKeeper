using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Item
{
    [SerializeField] private AudioSource _collectSound;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (!_itemHit)
            {
                PlayerBelongs.AddMoney(1);
                OnItemHit?.Invoke();
            }

            if (_collectSound != null)
            {
                _collectSound.Play();
                var o = _collectSound.gameObject;
                o.transform.parent = null;
                Destroy(o, 1f);
            }

            _itemHit = true;
            Destroy(gameObject);
        }
    }

    private void Awake()
    {
        _rigidbody.AddTorque(0f, 8f, 0f, ForceMode.VelocityChange);
    }
}