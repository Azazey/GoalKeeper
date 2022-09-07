using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bomb : Item
{
    [SerializeField] private GameObject _effect;
    [SerializeField] private AudioSource _explosion;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (!_itemHit)
            {
                PlayerBelongs.SubtractScore(5);
                OnItemHit?.Invoke();
            }

            _itemHit = true;
            Explode();
        }
    }

    private void Explode()
    {
        Instantiate(_effect, transform.position, Quaternion.identity);
        if (_explosion != null)
        {
            _explosion.Play();
            var o = _explosion.gameObject;
            o.transform.parent = null;
            Destroy(o, 1f);
        }

        Destroy(gameObject);
    }
}