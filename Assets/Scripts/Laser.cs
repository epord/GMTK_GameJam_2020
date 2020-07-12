using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject impactEffect;
    public float TimeUntilDisapear = 5f;

    [HideInInspector]
    public Vector3 laserDirection;

    [HideInInspector]
    public float speed;
    
    [HideInInspector]
    public int damage;
    
    // Start is called before the first frame update
    void Start()
    {
        laserDirection = laserDirection.normalized;
        rb.velocity = laserDirection * speed;
        Destroy(gameObject, TimeUntilDisapear);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Debug.Log(hitInfo.name);
        Destroyable destroyable = hitInfo.GetComponent<Destroyable>();
        if (destroyable != null)
        {
            destroyable.TakeHit(damage);
        }
        ErrorActivator playerError = hitInfo.GetComponent<ErrorActivator>();
        if (playerError != null)
        {
            playerError.TakeHit(damage);
        }
        /*Instantiate(impactEffect, transform.position, transform.rotation);*/
        Destroy(gameObject);
    }
    
    
    
}
