﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlasterGun : MonoBehaviour
{

    public Transform firePoint;

    public GameObject laserPrefab;

    public float laserSpeed = 20f;
    public int laserDamage = 1;

    public float delay = 0.0f; // seconds to shoot after click
    public float deviation = 0.0f; // in degrees
    public float cooldown = 0.5f; // in seconds

    private SpaceshipPlayer audioPlayer;
    private bool canShoot = true;

    void Start()
    {
        audioPlayer = GetComponentInChildren<SpaceshipPlayer>();
    }

    private IEnumerator Shoot()
    {
        canShoot = false;
        Vector3 target = Input.mousePosition;
        target.z = 0.0f;
        target = UnityEngine.Camera.main.ScreenToWorldPoint(target);
        target.z = 0.0f;
        return Shoot(target);
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldown);
        canShoot = true;
    }

        // Update is called once per frame
        void Update()
    {
        if (canShoot && (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)))
        {
            StartCoroutine(Cooldown());
            StartCoroutine(Shoot());
        }
    }
    
    IEnumerator Shoot(Vector3 target)
    {
        Vector3 shootDirection = target - firePoint.position;
        float driftAngle = Mathf.Cos(Time.time * 10.0f) * deviation * 2.0f * Mathf.PI / 360.0f;
        float finalAngle = Mathf.Atan2(shootDirection.y, shootDirection.x) + driftAngle;
        shootDirection = new Vector3(Mathf.Cos(finalAngle), Mathf.Sin(finalAngle), 0);
        shootDirection.Normalize();

        Quaternion laserRotation = Quaternion.FromToRotation(Vector3.up, shootDirection);
       
        
        yield return new WaitForSeconds(delay);
        Vector3 laserPosition = firePoint.position;
        Laser laser = Instantiate(laserPrefab, laserPosition, laserRotation).GetComponent<Laser>();
        Physics2D.IgnoreCollision(laser.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        laser.laserDirection = shootDirection;
        laser.speed = laserSpeed;
        laser.damage = laserDamage;

        audioPlayer.LaserShoot();
    }
}
