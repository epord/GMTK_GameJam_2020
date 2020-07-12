using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject laserPrefab;

    public float laserSpeed = 20f;
    public int laserDamage = 1;
    public float speed = 2f;
    public float deviation = 10.0f; // in degrees
    public float DeviationChance = 0.5f;

    public float minReloadTime = 1.5f;
    public float maxReloadTime = 4.0f;

    public GameObject target;

    public bool isShooting = false;
    public bool isInRange = false;

    
    public void InRange()
    {
        isInRange = true;
    }

    public void OutOfRange()
    {
        isInRange = false;
    }

    private IEnumerator ShootRoutine()
    {
        yield return new WaitForSeconds(Random.Range(minReloadTime, maxReloadTime));
        Shoot();
        isShooting = false;
    }

    private void Update()
    {
        if (!isShooting)
        {
            if (isInRange)
            {
                isShooting = true;
                StartCoroutine(ShootRoutine()); 
            }
            else
            {
                Chase();
            }
        }
    }

    void Chase()
    {
        Vector2 direction;
        if (target != null)
        {
            direction = target.transform.position - transform.position;
        }
        else
        {
            direction = Random.insideUnitCircle;
        }
        direction.Normalize();
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void Shoot()
    {
        Vector3 shootDirection = target.transform.position - transform.position;

        bool doesItDeviate = Random.Range(0.0f, 1.0f) > DeviationChance;
        if (doesItDeviate)
        {
            float driftAngle = Mathf.Cos(Time.time * 10.0f) * deviation * 2.0f * Mathf.PI / 360.0f;
            float finalAngle = Mathf.Atan2(shootDirection.y, shootDirection.x) + driftAngle;
            shootDirection = new Vector3(Mathf.Cos(finalAngle), Mathf.Sin(finalAngle), shootDirection.z);
        }
        shootDirection.Normalize();
        
        Quaternion laserRotation = Quaternion.FromToRotation(Vector3.up, shootDirection);
        Vector3 laserPosition = transform.position;

        Laser laser = Instantiate(laserPrefab, laserPosition, laserRotation).GetComponent<Laser>();
        
        // Ignore own colliders
        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach (Collider2D collider in colliders) Physics2D.IgnoreCollision(laser.GetComponent<Collider2D>(), collider);
        
        laser.laserDirection = shootDirection;
        laser.speed = laserSpeed;
        laser.damage = laserDamage;
        laser.layer = 
    }
}
