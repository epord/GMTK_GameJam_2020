using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject laserPrefab;

    public float laserSpeed = 20f;
    public int laserDamage = 1;
    public float speed = 0.6f;

    public float minReloadTime = 1.5f;
    public float maxReloadTime = 4.0f;

    public GameObject target;

    private void Start()
    {
        StartCoroutine(ShootRoutine());
    }

    public void StartChasing(GameObject target) {
        this.target = target;
    }

    public void StopChasing()
    {
        this.target = null;
    }

    private IEnumerator ShootRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minReloadTime, maxReloadTime));
            if (target != null) Shoot();
        }
    }

    private void Update()
    {
        if (target != null)
        {
            // Chase logic
            Vector2 direction = target.transform.position - transform.position;
            direction.Normalize();
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }

    void Shoot()
    {
        Vector3 shootDirection = target.transform.position - transform.position;
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
    }
}
