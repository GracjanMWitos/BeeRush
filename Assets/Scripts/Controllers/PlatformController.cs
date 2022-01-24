using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [SerializeField] private float platformMoveSpeed = 3f;
    void Update()
    {
        Camera.main.transform.position += Vector3.up * Time.deltaTime;
    }
}
