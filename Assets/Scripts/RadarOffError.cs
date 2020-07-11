using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarOffError : MonoBehaviour
{
    public bool isActive = false;

    private Radar radar;

    void Start()
    {
        radar = GetComponent<Radar>();
    }

    void Update()
    {
        if (isActive)
        {
            radar.isActive = false;
        }
        else
        {
            radar.isActive = true;
        }
    }
}
