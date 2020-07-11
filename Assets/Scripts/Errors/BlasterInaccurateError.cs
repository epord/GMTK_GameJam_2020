using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlasterDelayError : Error
{
    BlasterGun blasterGun;

    public float delay = 2.0f; // in seconds

    private void Start()
    {
        blasterGun = GetComponent<BlasterGun>();
    }

    public override void OnActivate()
    {
        blasterGun.delay = delay;
    }

    public override void OnDeactivate()
    {
        blasterGun.delay = 0.0f;
    }

}
