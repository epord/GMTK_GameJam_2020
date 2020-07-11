using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlasterInaccurateError : Error
{
    BlasterGun blasterGun;

    public float deviation = 10.0f; // in degrees

    private void Start()
    {
        blasterGun = GetComponent<BlasterGun>();
    }

    public override void OnActivate()
    {
        blasterGun.deviation = deviation;
    }

    public override void OnDeactivate()
    {
        blasterGun.deviation = 0.0f;
    }

}
