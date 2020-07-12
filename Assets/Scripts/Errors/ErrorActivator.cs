using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ErrorActivator : MonoBehaviour
{
    public Error[] errors;
    public float minTimeToBreak = 5.0f;
    public float maxTimeToBreak = 20.0f;
    public int newErrorMaxHP = 5;
    private int currentHP = 0;

    public AudioSource PartDestroyedByLaser;
    public AudioSource HitByLaser;
    public AudioSource GameOver;
    private bool gameOver = false;

    
    private void Start()
    {
        currentHP = newErrorMaxHP;
        StartCoroutine(RandomBreak());
    }

    public void FixAll()
    {
        foreach (Error error in errors)
        {
            error.Deactivate();
        }
    }

    public void Lose()
    {
        GameOver.Play();
        Debug.Log("YOU LOSE");
        Time.timeScale = 0;
        gameOver = true;
        StartCoroutine(RestartGame());
    }

    private IEnumerator RestartGame()
    {
        yield return new  WaitForSecondsRealtime(5.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void TakeHit(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            
            currentHP = newErrorMaxHP;
            List<Error> unactiveErrors = new List<Error>();
        
            foreach (Error error in errors)
            {
                if (!error.isActive) unactiveErrors.Add(error);
            }

            if (unactiveErrors.Count <= 0)
            {
                Lose();
            }
            else
            {
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
            foreach (Error error in errors)
            {
                if (!error.isActive) unactiveErrors.Add(error);
            }

            int idx = Random.Range(0, unactiveErrors.Count);
            if (idx < unactiveErrors.Count)
            {
                unactiveErrors[idx].Activate();
                Debug.Log("Activating " + unactiveErrors[idx].GetType());
            }

        }
    }

    void Update()
    {
        

        if (Input.GetKeyDown("1"))
        {
            errors[0].Toggle();
        }
        if (Input.GetKeyDown("2"))
        {
            errors[1].Toggle();
        }
        if (Input.GetKeyDown("3"))
        {
            errors[2].Toggle();
        }
        if (Input.GetKeyDown("4"))
        {
            errors[3].Toggle();
        }
        if (Input.GetKeyDown("5"))
        {
            errors[4].Toggle();
        }
        if (Input.GetKeyDown("6"))
        {
            errors[5].Toggle();
        }
        if (Input.GetKeyDown("7"))
        {
            errors[6].Toggle();
        }
        if (Input.GetKeyDown("8"))
        {
            errors[7].Toggle();
        }
        if (Input.GetKeyDown("9"))
        {
            errors[8].Toggle();
        }
        if (Input.GetKeyDown("0"))
        {
            errors[9].Toggle();
        }
    }
}
