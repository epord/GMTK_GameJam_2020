using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlasterGun : MonoBehaviour
{

    public Transform firePoint;

    public GameObject laserPrefab;

    public float laserSpeed = 20f;
    public int laserDamage = 1;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 target = Input.mousePosition;
            target.z = 0.0f;
            target =  UnityEngine.Camera.main.ScreenToWorldPoint(target);
            target.z = 0.0f;
            Shoot(target);
        }
    }

    
    void Shoot(Vector3 target)
    {
        Vector3 shootDirection = target-firePoint.position;
    
        Quaternion laserRotation = Quaternion.FromToRotation(Vector3.up, shootDirection);
        Vector3 laserPosition = firePoint.position;

        
        Laser laser = Instantiate(laserPrefab, laserPosition, laserRotation).GetComponent<Laser>();
        laser.laserDirection = shootDirection;
        laser.speed = laserSpeed;
        laser.damage = laserDamage;
    }
}
