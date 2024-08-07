using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereMovement : MonoBehaviour
{
    [SerializeField] float speed = 1;


    void Update()
    {
        transform.position = transform.position + transform.forward * speed * Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        transform.Rotate(Vector3.up * 45 * (Random.Range(0, 2) == 0? 1 : -1));
    }
}
