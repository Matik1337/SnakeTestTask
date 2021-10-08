using UnityEngine;

public class Crystal : MonoBehaviour
{
    private void FixedUpdate()
    {
        transform.RotateAround(transform.position, Vector3.up, 3);
    }
}
