using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialTintColor : MonoBehaviour
{

    public Color tintColor;
    public bool isColorActive = false;
    public float alphaActive = 0f;
    public float alphaSpeed = 0f;
    public float alphaIntensity = 0f;

    private Material material;

    private void Start()
    {
        material = gameObject.GetComponent<SpriteRenderer>().material;
    }
    
    // Update is called once per frame
    void Update()
    {
        Color newTintColor;
        
        if (isColorActive)
        {
            newTintColor = tintColor;
        }
        else
        {
            newTintColor = new Color(0,0,0,0);        
        }

        float newAlpha = alphaActive;
        newAlpha = Mathf.Clamp01(alphaActive + Mathf.Sin(Time.time * alphaSpeed) * alphaIntensity);
        newTintColor.a = newAlpha;
        material.SetColor("_Tint", newTintColor);
    }
}
