using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarInaccurateError : MonoBehaviour
{
    public bool isActive = false;
    public float inaccuracy = 30.0f; // angle of inaccuracy

    private Radar radar;

    void Start()
    {
        radar = GetComponent<Radar>();
    }

    void Update()
    {
        if (isActive)
        {
            radar.inaccuracy = inaccuracy;
        } else
        {
            radar.inaccuracy = 0.0f;
        }
    }
}
