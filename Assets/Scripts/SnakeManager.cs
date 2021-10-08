using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeManager : MonoBehaviour
{
    [SerializeField] private List<SnakeLink> _body;
    [SerializeField] private SnakeLink _link;
    [SerializeField] private Material _currentMaterial;
    [SerializeField] private float _coloringDelay;

    private void Awake()
    {
        _body = new List<SnakeLink>();
        _body.Add(GetComponent<SnakeLink>());
    }

    private void Start()
    {
        AddLink();
        AddLink();
    }

    public void AddLink()
    {
        if (_body.Count < 10)
        {
            _body.Add(Instantiate(_link, _body[_body.Count - 1].transform.position, Quaternion.identity));
        
            _body[_body.Count - 1].SetTarget(_body[_body.Count - 2]);
        }
    }

    public void ChangeColor(Material material)
    {
        _currentMaterial = material;

        StartCoroutine(ChangeAllMaterials());
    }

    private IEnumerator ChangeAllMaterials()
    {
        foreach (var link in _body)
        {
            link.GetComponent<MeshRenderer>().material = _currentMaterial;
            
            yield return new WaitForSeconds(_coloringDelay);
        }
    }
}
