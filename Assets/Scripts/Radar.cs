using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
    public GameObject[] targets;

    private void FixedUpdate()
    {
        int layerMask = 0;


        //Debug.DrawRay(transform.position, target.transform.position - transform.position);
        foreach (GameObject target in targets)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, target.transform.position - transform.position);
            if (hit != null)
            {
                if (Vector2.Distance(transform.position, target.transform.position) > hit.distance)
                {
                    Debug.DrawLine(hit.point, transform.position, Color.green);
                }
            }
        }
    }
}
