using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [SerializeField] private float platformMoveSpeed = 3f;
    void Update()
    {
        transform.position += Vector3.down * Time.deltaTime;
    }
}
