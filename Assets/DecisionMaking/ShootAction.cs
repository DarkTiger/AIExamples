using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAction : MonoBehaviour
{
    public GameObject bulletPrefab;

	public void Shoot ()
    {
        var bulletGo = Instantiate(bulletPrefab);
        bulletGo.transform.position = transform.position + transform.right * 0.25f;
        bulletGo.transform.rotation = transform.rotation;
	}


    public float shootRate = 1.0f;

    internal void StartShooting()
    {
        InvokeRepeating("Shoot", shootRate, shootRate);
    }

    internal void StopShooting()
    {
        CancelInvoke("Shoot");
    }
}
