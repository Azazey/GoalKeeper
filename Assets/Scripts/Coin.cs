using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Item
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (!_itemHit)
            {
                PlayerBelongs.AddMoney(1);
                OnItemHit?.Invoke();   
            }

            _itemHit = true;
            Destroy(gameObject);
        }
    }
}
