using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MouthTrigger : MonoBehaviour
{
    [SerializeField] private Vector3 _defaultScale;
    [SerializeField] private Vector3 _feverScale;
    
    private SnakeManager _snakeManager;
    private int _deadCount;
    private int _crystalsCount;
    private int _crystalsStrik;
    private float _deleteObjDelay;
    private bool _isInFever;
    private float _feverDelay;


    public UnityAction ActivateFever;
    public UnityAction DeactivateFever;
    public UnityAction<int> CrystalEaten;
    public UnityAction<int> HumanEaten;

    private void Awake()
    {
        _snakeManager = GetComponentInParent<SnakeManager>();
        _deadCount = 0;
        _crystalsCount = 0;
        _crystalsStrik = 0;
        _deleteObjDelay = 0.3f;
        _feverDelay = 5f;
    }

    private void OnTriggerEnter(Collider other)
    {
        EatHuman(other);
        EatCrystal(other);
        
        if(_isInFever)
            EatObstacle(other);
    }

    private void EatHuman(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Human human))
        {
            if (human.GetComponent<MeshRenderer>().material.color ==
                _snakeManager.GetComponent<MeshRenderer>().material.color || _isInFever)
            {
                human.Die(_snakeManager.transform);
                _snakeManager.AddLink();
                _deadCount++;

                HumanEaten?.Invoke(_deadCount);
            }
            else
            {
                SceneManager.LoadScene(0);
            }

            _crystalsStrik = 0;
        }
    }

    private void EatCrystal(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Crystal crystal))
        {
            DeleteObject(crystal.gameObject);
            
            if (_crystalsStrik > 3 && !_isInFever)
            {
                EnableFever();
            }
        }
    }

    private void EatObstacle(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Obstacle obstacle))
        {
            DeleteObject(obstacle.gameObject);
        }
    }
    
    private void DeleteObject(GameObject obj)
    {
        _crystalsCount++;
        _crystalsStrik++;
        
        obj.GetComponent<Collider>().isTrigger = true;
        obj.transform.DOScale(Vector3.zero, _deleteObjDelay);
        obj.transform.DOMove(transform.position, _deleteObjDelay);
            
        Destroy(obj.gameObject, _deleteObjDelay);
            
        CrystalEaten?.Invoke(_crystalsCount);
    }
    
    private void EnableFever()
    {
        ActivateFever?.Invoke();
        _isInFever = true;
        transform.localScale = _feverScale;
        Invoke(nameof(DisableFever), _feverDelay);
    }

    private void DisableFever()
    {
        DeactivateFever?.Invoke();
        _isInFever = false;
        transform.localScale = _defaultScale;
        _crystalsStrik = 0;
    }
}
