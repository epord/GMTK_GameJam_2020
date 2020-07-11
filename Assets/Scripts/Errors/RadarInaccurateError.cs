using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarInaccurateError : Error
{
    public float inaccuracy = 30.0f; // angle of inaccuracy

    private Radar radar;

    void Start()
    {
        radar = GetComponent<Radar>();
    }

    public override void OnActivate()
    {
        radar.inaccuracy = inaccuracy;
    }

    public override void OnDeactivate()
    {
        radar.inaccuracy = 0.0f;
    }

}
