using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : Item
{
    [SerializeField] private AudioSource _kickSound;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (!_itemHit)
            {
                PlayerBelongs.AddScore(1);
                OnItemHit?.Invoke();
            }

            _itemHit = true;
            Ricochet();
        }
    }

    private void Ricochet()
    {
        _rigidbody.AddForce(-_directionToTarget.normalized * _force, ForceMode.VelocityChange);
        _kickSound.Play();
    }
}