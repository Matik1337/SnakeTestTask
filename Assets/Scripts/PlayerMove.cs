using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _maxSidewaysDelta;
    [SerializeField] private float _sidewaysSpeed;
    [SerializeField] private float _feverSpeedMyltiplyer;

    private Rigidbody _rigidbody;
    private Vector3 _lastMousePosition;
    private MouthTrigger _mouthTrigger;
    private bool _isInFever;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _mouthTrigger = GetComponentInChildren<MouthTrigger>();
    }

    private void Start()
    {
        _lastMousePosition = Vector3.zero;
        
        SetForwardForce(_speed);
    }
    
    private void OnEnable()
    {
        _mouthTrigger.ActivateFever += OnActivateFever;
        _mouthTrigger.DeactivateFever += OnDeactivateFever;
    }

    private void OnDisable()
    {
        _mouthTrigger.ActivateFever -= OnActivateFever;
        _mouthTrigger.DeactivateFever -= OnDeactivateFever;
    }

    private void OnActivateFever()
    {
        _isInFever = true;
        
        SetForwardForce(_speed * _feverSpeedMyltiplyer);
    }

    private void OnDeactivateFever()
    {
        _isInFever = false;
        
        SetForwardForce(_speed);
    }

    private void FixedUpdate()
    {
        if(_isInFever)
            MoveCenter();
        else
            HandleSidewaysMoving();
    }

    private void MoveCenter()
    {
        Vector3 newPosition = new Vector3(0, transform.position.y, transform.position.z);
            
        transform.position = Vector3.Lerp(transform.position, newPosition, _sidewaysSpeed);
    }

    private void HandleSidewaysMoving()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (Math.Abs(hit.point.x) <= _maxSidewaysDelta)
                {
                    _lastMousePosition = hit.point;
                }
            }
        }
        
        if (transform.position.x != _lastMousePosition.x)
        {
            Vector3 newPosition = new Vector3(_lastMousePosition.x, transform.position.y, transform.position.z);
            
            transform.position = Vector3.Lerp(transform.position, newPosition, _sidewaysSpeed);
        }
    }

    private void SetForwardForce(float force)
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.AddForce(Vector3.forward * force, ForceMode.VelocityChange);
    }
}
