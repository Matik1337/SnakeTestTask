using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private PlayerMove _target;
    [SerializeField] private float _followSpeed;
    [SerializeField] private Vector3 _offcetPosition;
    [SerializeField] private Vector3 _offcetRotation;

    private void FixedUpdate()
    {
        Vector3 newPos = _target.transform.position;

        newPos = new Vector3(0, newPos.y, newPos.z);
        transform.DOLookAt(newPos + _offcetRotation, 0);
        newPos += _offcetPosition;
        
        transform.position = Vector3.Lerp(transform.position, newPos, _followSpeed);
        
    }
}
