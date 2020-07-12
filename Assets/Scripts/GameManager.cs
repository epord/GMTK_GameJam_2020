using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Planet[] planets;
    private bool won = false;
    private bool gameStarted = false;

    private IEnumerator Win()
    {
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("WinScreen");
    }

    void Update()
    {
        if (Input.anyKeyDown && !gameStarted)
        {
            Time.timeScale = 1.0f;
            gameStarted = true;
        }
        if (won) return;
        bool areAllPlanetsVisited = true;
        foreach (Planet planet in planets)
        {
            areAllPlanetsVisited = areAllPlanetsVisited && planet.isVisited;
        }

        if (areAllPlanetsVisited)
        {
            won = true;
            StartCoroutine(Win());
        }
        
    }
}
