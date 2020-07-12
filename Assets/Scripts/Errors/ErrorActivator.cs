using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ErrorActivator : MonoBehaviour
{
    public Error[] motorErrors = new Error[2];
    public Error[] driveErrors = new Error[2];
    public Error[] radarErrors = new Error[2];
    public Error[] blasterErrors = new Error[2];

    [HideInInspector]
    public List<Error> errors = new List<Error>();

    public float minTimeToBreak = 5.0f;
    public float maxTimeToBreak = 20.0f;
    public int newErrorMaxHP = 5;
    private int currentHP = 0;

    public GameObject PlayerDead;
    public AudioSource PartDestroyedByLaser;
    public AudioSource HitByLaser;
    public AudioSource GameOver;
    private bool gameOver = false;

    [HideInInspector]
    public int motorDamageLevel = 0;
    public int driveDamageLevel = 0;
    public int radarDamageLevel = 0;
    public int blasterDamageLevel = 0;

    private void Update()
    {
        motorDamageLevel = 0;
        driveDamageLevel = 0;
        radarDamageLevel = 0;
        blasterDamageLevel = 0;

        foreach (Error error in motorErrors)
        {
            if (error.isActive) motorDamageLevel++;
        }

        foreach (Error error in driveErrors)
        {
            if (error.isActive) driveDamageLevel++;
        }

        foreach (Error error in radarErrors)
        {
            if (error.isActive) radarDamageLevel++;
        }

        foreach (Error error in blasterErrors)
        {
            if (error.isActive) blasterDamageLevel++;
        }

    }

    private void Start()
    {
        errors.AddRange(motorErrors);
        errors.AddRange(driveErrors);
        errors.AddRange(radarErrors);
        errors.AddRange(blasterErrors);
        currentHP = 1;
        StartCoroutine(RandomBreak());
    }

    public void FixAll()
    {
        foreach (Error error in motorErrors) error.Deactivate();
        foreach (Error error in driveErrors) error.Deactivate();
        foreach (Error error in radarErrors) error.Deactivate();
        foreach (Error error in blasterErrors) error.Deactivate();

        gameObject.GetComponent<MaterialTintColor>().isColorActive = false;
        currentHP = 1;
    }

    public void Lose()
    {
        GameOver.Play();
        Debug.Log("YOU LOSE");
        Time.timeScale = 0f;
        gameOver = true;
       
        gameObject.SetActive(false);
        Instantiate(PlayerDead, transform.position, transform.rotation);
    }

   

    public void TakeHit(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            
            currentHP = newErrorMaxHP;
            List<Error> unactiveErrors = new List<Error>();

            Error[][] errorsList = { motorErrors, driveErrors, radarErrors, blasterErrors };
            foreach (Error[] errorList in errorsList)
            {
                foreach (Error error in errorList)
                {
                    if (!error.isActive)
                    {
                        unactiveErrors.Add(error);
                        break;
                    }
                }
            }

            if (unactiveErrors.Count <= 0)
            {
                Lose();
            }
            else
            {
                //if (unactiveErrors.Count <= 4)
                //{
                //    gameObject.GetComponent<MaterialTintColor>().isColorActive = true;
                //}
                
                PartDestroyedByLaser.Play();
                int idx = Random.Range(0, unactiveErrors.Count);
                if (idx < unactiveErrors.Count)
                {
                    unactiveErrors[idx].Activate();
                    Debug.Log("Activating " + unactiveErrors[idx].GetType());
                }
            }
        }
        else
        {
            HitByLaser.Play();
        }
    }

    private IEnumerator RandomBreak()
    {

        while (true)
        {
            float wait = Random.Range(minTimeToBreak, maxTimeToBreak);
            yield return new WaitForSeconds(wait);

            List<Error> unactiveErrors = new List<Error>();
            Error[][] errorsList = { motorErrors, driveErrors, radarErrors, blasterErrors };
            foreach (Error[] errorList in errorsList)
            {
                foreach (Error error in errorList)
                {
                    if (!error.isActive)
                    {
                        unactiveErrors.Add(error);
                        break;
                    }
                }
            }

            int idx = Random.Range(0, unactiveErrors.Count);
            if (idx < unactiveErrors.Count)
            {
                unactiveErrors[idx].Activate();
            }
            if (unactiveErrors.Count <= 4)
            {
                gameObject.GetComponent<MaterialTintColor>().isColorActive = true;
            }

        }
    }
}
