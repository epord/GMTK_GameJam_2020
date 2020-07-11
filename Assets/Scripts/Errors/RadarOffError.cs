using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarOffError : Error
{
    private Radar radar;

    void Start()
    {
        radar = GetComponent<Radar>();
    }

    public override void OnActivate()
    {
        Debug.Log("Activate");
        radar.isActive = false;
    }

    public override void OnDeactivate()
    {
        Debug.Log("Deactivate");
        radar.isActive = true;
    }
}
