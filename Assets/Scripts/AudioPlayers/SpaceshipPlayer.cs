using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipPlayer : MonoBehaviour
{
    public AudioSource turboLoop;
    public AudioSource turboFadeIn;
    public AudioSource laserShoot;

    public float FADE_STEP = 0.07f;
    public float BASE_TURBO_LOOP_VOLUME = 0.2f;
    public float FAST_TURBO_LOOP_VOLUME = 0.8f;
    public float TURBO_FADE_IN_VOLUME = 0.8f;
    public float DELAY_VOICES = 6.0f;

    private IEnumerator startTurboRoutine;
    private ErrorActivator errorActivator;

    private void Start()
    {
        errorActivator = GetComponentInParent<ErrorActivator>();

        StartCoroutine(GenerateErrorVoicesList());
        StartCoroutine(PlayErrorVoices());

        turboFadeIn.volume = TURBO_FADE_IN_VOLUME;
        turboFadeIn.Stop();
        turboLoop.volume = BASE_TURBO_LOOP_VOLUME;
        turboLoop.Play();
    }

    private IEnumerator StartTurboRoutine()
    {
        turboFadeIn.Play();
        turboLoop.Play();
        while (turboLoop.volume < FAST_TURBO_LOOP_VOLUME)
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
        StopCoroutine(startTurboRoutine);
        StartCoroutine(StopTurboRoutine());
    }

    public void LaserShoot()
    {
        laserShoot.pitch = Random.Range(1.0f, 1.70f);
        laserShoot.Play();
    }

    // == ERROR VOICES ===
    // Assusmes each error has voices in 7 different languages

    private List<AudioSource> voices = new List<AudioSource>();

    private IEnumerator GenerateErrorVoicesList()
    {
        while (true)
        {
            voices = new List<AudioSource>();
            for (int i = 0; i < errorActivator.errors.Count; i++)
            {
                Error error = errorActivator.errors[i];
                if (error.isActive)
                {
                    voices.AddRange(error.voiceErrors);
                }
            }
            yield return new WaitForSeconds(DELAY_VOICES);
        }
    }

    private IEnumerator PlayErrorVoices()
    {
        int i = 0;
        while (true)
        {
            if (i < voices.Count)
            {
                voices[i].Play();
            }
            i = Random.Range(0, voices.Count);
            yield return new WaitForSeconds(DELAY_VOICES);
        }
    }

}
