using System;
using DG.Tweening;
using UnityEngine;

public class SnakeLink : MonoBehaviour
{
    [SerializeField] private SnakeLink _target;
    [SerializeField] private float _speed;

    private void FixedUpdate()
    {
        if(_target != null)
            transform.position = Vector3.Lerp(transform.position, _target.transform.position, _speed);
    }

    public void SetTarget(SnakeLink target)
    {
        _target = target;
    }
}
