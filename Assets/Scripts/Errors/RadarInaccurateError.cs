using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarInaccurateError : Error
{
    public float deviation = 30.0f; // angle of inaccuracy

    private Radar radar;

    void Start()
    {
        radar = GetComponent<Radar>();
    }

    public override void OnActivate()
    {
        radar.deviation = deviation;
    }

    public override void OnDeactivate()
    {
        radar.deviation = 0.0f;
    }

}
