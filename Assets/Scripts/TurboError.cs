using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurboError : MonoBehaviour
{
    public bool isActive = false;

    public float minTurboTime = 0.5f;
    public float maxTurboTime = 2.0f;

    private Movement movement;
    private IEnumerator coroutine;
    private bool isCoroutineRunning = false;

    void Start()
    {
        movement = GetComponent<Movement>();
    }

    private IEnumerator DeactivateError()
    {
        while (true)
        {
            movement.speed = movement.TURBO_SPEED;
            yield return new WaitForSeconds(Random.Range(minTurboTime, maxTurboTime));
            movement.speed = movement.NORMAL_SPEED;
            yield return new WaitForSeconds(Random.Range(minTurboTime, maxTurboTime));
        }
    }

    void Update()
    {
        if (isActive)
        {
            if (!isCoroutineRunning)
            {
                isCoroutineRunning = true;
                coroutine = DeactivateError();
                StartCoroutine(coroutine);
            } 
        } else
        {
            if (coroutine != null) StopCoroutine(coroutine);
        }
        
    }
}
