using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyDeadAnimation : MonoBehaviour
{
    private Material material;

    private float fade = 1.0f;
    private float dissolveSpeed = 1.0f;

    public bool isPlayer = false;
    private bool restarting = false;
    
    
    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<SpriteRenderer>().material;        
    }

    
    void Update()
    {
        if (!isPlayer)
        {
            fade -= Time.deltaTime * dissolveSpeed;
        }
        else
        {
            if (!restarting)
            {
                restarting = true;
                
                StartCoroutine(RestartGame());
            }
            fade -= Time.unscaledDeltaTime * dissolveSpeed;
        }
        material.SetFloat("_Fade", fade);
        if (fade <= 0)
        {
            if (!isPlayer)
            {
                Destroy(gameObject);
            }
            else
            {
                fade = 0;
            }
        }
    }

    private IEnumerator RestartGame()
    {
        yield return new  WaitForSecondsRealtime(6.0f);
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
  
}
