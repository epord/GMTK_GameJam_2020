using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipPlayer : MonoBehaviour
{
    public AudioSource turboLoop;
    public AudioSource turboFadeIn;

    public float FADE_STEP = 0.07f;
    public float BASE_TURBO_LOOP_VOLUME = 0.2f;
    public float TURBO_FADE_IN_VOLUME = 0.8f;

    private IEnumerator startTurboRoutine;

    private void Start()
    {
        turboFadeIn.volume = TURBO_FADE_IN_VOLUME;
        turboFadeIn.Stop();
        turboLoop.volume = BASE_TURBO_LOOP_VOLUME;
        turboLoop.Play();
    }

    private IEnumerator StartTurboRoutine()
    {
        turboFadeIn.Play();
        turboLoop.Play();
        while (turboLoop.volume < 1.0f)
        {
            yield return new WaitForSeconds(0.1f);
            turboLoop.volume += FADE_STEP;
        }
    }

    public void StartTurbo()
    {
        startTurboRoutine = StartTurboRoutine();
        StartCoroutine(startTurboRoutine);
    }

    private IEnumerator StopTurboRoutine()
    {
        while (turboLoop.volume > BASE_TURBO_LOOP_VOLUME || turboFadeIn.volume > 0.0f)
        {
            yield return new WaitForSeconds(0.1f);
            turboLoop.volume = Mathf.Max(BASE_TURBO_LOOP_VOLUME, turboLoop.volume - FADE_STEP);
            turboFadeIn.volume = Mathf.Max(0.0f, turboFadeIn.volume - FADE_STEP);
        }
        turboFadeIn.Stop();
    }

    public void StopTurbo()
    {
        Debug.Log("stop turbo");
        StopCoroutine(startTurboRoutine);
        StartCoroutine(StopTurboRoutine());
    }
}
