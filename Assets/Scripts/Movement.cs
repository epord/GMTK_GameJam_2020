using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Constants that can be edited from UI
    public float SLOW_SPEED = 0.1f;
    public float NORMAL_SPEED = 1f;
    public float TURBO_SPEED = 3.0f;

    public float speed;

    public float angle = 0.0f; // in degrees
    public float angleSpeed = 1.0f; // degrees per frame

    void Start()
    {
        speed = NORMAL_SPEED;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Left"))
        {
            angle = (angle + angleSpeed + 360) % 360;
        }


        if (Input.GetButton("Right"))
        {
            angle = (angle - angleSpeed + 360) % 360;
        }

        if (Input.GetButton("Turbo"))
        {
            speed = TURBO_SPEED;
        } else
        {
            speed = NORMAL_SPEED;
        }

        transform.rotation = Quaternion.Euler(0, 0, angle);
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }
}
