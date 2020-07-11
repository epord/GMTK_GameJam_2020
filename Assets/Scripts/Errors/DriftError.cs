using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriftError : Error
{
    public float minDriftAmount = 0.2f;
    public float maxDriftAmount = 1.0f;

    private Movement movement;
    private float driftAmount;


    void Start()
    {
        movement = GetComponent<Movement>();    
    }

    public override void OnActivate()
    {
        int sign = Random.Range(0, 2) < 1 ? -1 : 1;
        driftAmount = sign * Random.Range(minDriftAmount, maxDriftAmount);
    }

    public override void Apply()
    {
        movement.angle += driftAmount;
    }
}
