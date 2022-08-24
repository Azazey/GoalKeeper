using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDrag : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    private bool _dragged;

    private void Update()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 20f, Color.red);
    
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    _dragged = true;
                }
            }
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            _dragged = false;
        }
    
        if (_dragged)
        {
            transform.position = new Vector3(Mathf.Clamp(ray.direction.x, -2.5f, 2.5f),
                transform.position.y, transform.position.z);
        }
    }

}