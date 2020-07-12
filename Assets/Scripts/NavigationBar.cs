using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationBar : MonoBehaviour
{
    public ErrorActivator errorActivator;

    public SpriteRenderer radarOK;
    public SpriteRenderer radarMedium;
    public SpriteRenderer radarBroken;

    public SpriteRenderer driftOK;
    public SpriteRenderer driftMedium;
    public SpriteRenderer driftBroken;

    public SpriteRenderer blasterOK;
    public SpriteRenderer blasterMedium;
    public SpriteRenderer blasterBroken;

    public SpriteRenderer motorOK;
    public SpriteRenderer motorMedium;
    public SpriteRenderer motorBroken;

    void Update()
    {
        // Radar error
        if (errorActivator.radarDamageLevel == 0)
        {
            radarMedium.enabled = false;
            radarBroken.enabled = false;
        }
        else if (errorActivator.radarDamageLevel == 1)
        {
            radarMedium.enabled = true;
            radarBroken.enabled = false;
        }
        else if (errorActivator.radarDamageLevel == 2)
        {
            radarMedium.enabled = false;
            radarBroken.enabled = true;
        }

        // Drift error
        if (errorActivator.driveDamageLevel == 0)
        {
            driftMedium.enabled = false;
            driftBroken.enabled = false;
        }
        else if (errorActivator.driveDamageLevel == 1)
        {
            driftMedium.enabled = true;
            driftBroken.enabled = false;
        }
        else if (errorActivator.driveDamageLevel == 2)
        {
            driftMedium.enabled = false;
            driftBroken.enabled = true;
        }

        // Blaster error
        if (errorActivator.blasterDamageLevel == 0)
        {
            blasterMedium.enabled = false;
            blasterBroken.enabled = false;
        }
        else if (errorActivator.blasterDamageLevel == 1)
        {
            blasterMedium.enabled = true;
            blasterBroken.enabled = false;
        }
        else if (errorActivator.blasterDamageLevel == 2)
        {
            blasterMedium.enabled = false;
            blasterBroken.enabled = true;
        }

        // Motor error
        if (errorActivator.motorDamageLevel == 0)
        {
            motorMedium.enabled = false;
            motorBroken.enabled = false;
        }
        else if (errorActivator.motorDamageLevel == 1)
        {
            motorMedium.enabled = true;
            motorBroken.enabled = false;
        }
        else if (errorActivator.motorDamageLevel == 2)
        {
            motorMedium.enabled = false;
            motorBroken.enabled = true;
        }
    }
}
