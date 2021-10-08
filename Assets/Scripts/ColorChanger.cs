using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    private Material _material;

    private void Awake()
    {
        _material = GetComponent<MeshRenderer>().material;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out SnakeManager snakeManager))
        {
            snakeManager.ChangeColor(_material);
        }
    }
}
