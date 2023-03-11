using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public int damage = 1;
    public GameObject impactEffect;
    public LineRenderer line;
    public Animator player;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !PlayerController.instance.isDie)
        {
            if (PlayerController.instance.PlayerEnabled)
            {
                Shoot();
            }
        }
    }

    void Shoot()
    {
        player.SetTrigger("Shoot");
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
