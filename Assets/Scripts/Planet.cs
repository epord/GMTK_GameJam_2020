using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public Shader visitedShader;

    [HideInInspector]
    public bool isVisited = false;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!isVisited && collider.tag == "Player") {
            ErrorActivator errorActivator;
            collider.gameObject.TryGetComponent<ErrorActivator>(out errorActivator);
            if (errorActivator != null)
            {
                errorActivator.FixAll();
                isVisited = true;
                SpriteRenderer renderer = GetComponent<SpriteRenderer>();
                renderer.material.shader = visitedShader;

            }
        }
    }
}
