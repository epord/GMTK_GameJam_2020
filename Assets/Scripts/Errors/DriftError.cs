using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriftError : Error
{
    public float driftAmount = 2.0f;

    private Movement movement;

    void Start()
    {
        movement = GetComponent<Movement>();    
    }

    public override void Apply()
    {
        movement.angle += Random.Range(-driftAmount, driftAmount);
    }
}
