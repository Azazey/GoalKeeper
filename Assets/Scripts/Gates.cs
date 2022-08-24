using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gates : MonoBehaviour
{

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Item"))
        {
            if (collider.GetComponentInParent<Ball>())
            {
                PlayerBelongs.SubtractScore(1);
                collider.GetComponentInParent<Ball>().OnItemHit?.Invoke();
            }
        }
    }
}
