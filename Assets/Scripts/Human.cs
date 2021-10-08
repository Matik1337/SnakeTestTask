using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class Human : MonoBehaviour
{
    [SerializeField] private float _dyingDelay;
    
    public UnityAction Dying;

    public void Die(Transform target)
    {
        Dying?.Invoke();
        
        transform.DOMove(target.position, _dyingDelay);
        transform.DOScale(Vector3.zero, _dyingDelay);
        
        Destroy(gameObject, _dyingDelay);
    }
}
