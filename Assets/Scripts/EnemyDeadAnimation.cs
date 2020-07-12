using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadAnimation : MonoBehaviour
{
    private Material material;

    private float fade = 1.0f;
    private float dissolveSpeed = 1.0f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<SpriteRenderer>().material;        
    }

    
    void Update()
    {
        fade -= Time.deltaTime * dissolveSpeed;
        material.SetFloat("_Fade", fade);
        if (fade <= 0)
        {
            Destroy(gameObject);
        }
    }

  
}
