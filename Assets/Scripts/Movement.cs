using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Constants that can be edited from UI
    public float SLOW_SPEED = 0.1f;
    public float NORMAL_SPEED = 1f;
    public float TURBO_SPEED = 3.0f;

    public float angleSpeed = 1.0f; // degrees per frame

    public SpriteRenderer leftFire;
    public SpriteRenderer centerFire;
    public SpriteRenderer rightFire;

    [HideInInspector]
    public float speed;

    [HideInInspector]
    public float angle = 0.0f; // in degrees

    private SpaceshipPlayer audioPlayer;

    void Start()
    {
        audioPlayer = GetComponentInChildren<SpaceshipPlayer>();
        speed = NORMAL_SPEED;
    }

    public void StartTurbo()
    {
        centerFire.enabled = true;
        audioPlayer.StartTurbo();
        speed = TURBO_SPEED;
    }

    public void StopTurbo()
    {
        centerFire.enabled = false;
        audioPlayer.StopTurbo();
        speed = NORMAL_SPEED;
    }

    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, angle);
        transform.Translate(Vector2.up * speed * Time.deltaTime);

        leftFire.enabled = true;
        rightFire.enabled = true;

        if (Input.GetButton("Left"))
        {
            leftFire.enabled = false;
            angle = (angle + angleSpeed + 360) % 360;
        }


        if (Input.GetButton("Right"))
        {
            rightFire.enabled = false;
            angle = (angle - angleSpeed + 360) % 360;
        }

        if (Input.GetButtonDown("Turbo"))
        {
            StartTurbo();
        }

        if (Input.GetButtonUp("Turbo"))
        {
            StopTurbo();
        }
    }
}
