using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bomb : Item
{
    [SerializeField] private GameObject _effect;
    
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
        Destroy(gameObject);
    }
}
