using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriftError : MonoBehaviour
{
    public bool isActive = false;
    public float driftAmount = 2.0f;

    private Movement movement;

    void Start()
    {
        movement = GetComponent<Movement>();    
    }

    void Update()
    {
        if (isActive)
        {
            movement.angle += Random.Range(-driftAmount, driftAmount);
        }
    }
}
