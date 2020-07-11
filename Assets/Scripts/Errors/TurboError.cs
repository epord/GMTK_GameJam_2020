using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurboError : Error
{
    public float minTurboTime = 0.5f;
    public float maxTurboTime = 2.0f;

    private Movement movement;
    private IEnumerator coroutine;

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

    public override void OnActivate()
    {
        coroutine = DeactivateError();
        StartCoroutine(coroutine);
    }

    public override void OnDeactivate()
    {
        if (coroutine != null) StopCoroutine(coroutine);
        movement.speed = movement.NORMAL_SPEED;
    }
}
