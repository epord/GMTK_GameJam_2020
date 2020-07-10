using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 0.5f;

    public float angle = 0.0f; // in degrees
    public float angleSpeed = 1.0f; // degrees per frame

    void Start()
    {
        
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

        transform.rotation = Quaternion.Euler(0, 0, angle);
        transform.Translate(Vector2.up * Time.deltaTime);

        //float radAngle = angle * 2 * Mathf.PI / 360.0f;
        //Vector2 direction = new Vector2(Mathf.Sin(radAngle) * speed, Mathf.Cos(radAngle) * speed);
        //transform.Translate(direction * Time.deltaTime);
    }
}
