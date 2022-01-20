using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Speed")]
    [SerializeField] private float flyingSpeed = 2f;
    void Update()
    {
   
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = new Vector3(Mathf.Clamp(mousePosition.x, -15.75f, 15.75f), Mathf.Clamp(mousePosition.y, -8.5f, 8.5f), 0);
        transform.position = Vector3.Lerp(transform.position, direction, flyingSpeed * Time.deltaTime);
    }
}
