﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
    public GameObject[] targets;
    public float activationTime = 3.0f; // time in seconds of how long the radar will be active
    public float cooldown = 30.0f; // time in seconds the player has to wait before using the radar again

    private List<GameObject> targetIcons = new List<GameObject>();
    private bool isActive = true;
    private bool canActivate = true;

    private IEnumerator ActivateRadar()
    {
        isActive = true;
        yield return new WaitForSeconds(activationTime);
        isActive = false;
        canActivate = false;
        yield return new WaitForSeconds(cooldown);
        canActivate = true;
    }

    private void Update()
    {
        // Uncomment to use a key to activate radar
        //if (canActivate && Input.GetButtonDown("Radar"))
        //{
        //    StartCoroutine(ActivateRadar());
        //}
    }

    private void FixedUpdate()
    {
        // Remove previous target icons
        foreach (GameObject targetIcon in targetIcons)
        {
            Destroy(targetIcon);
        }
        targetIcons.Clear();

        if (!isActive) return; // skip finding the targets locations

        foreach (GameObject target in targets)
        {
            Vector2 direction = target.transform.position - transform.position;
            direction.Normalize();
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction);
            if (hit != null)
            {
                if (Vector2.Distance(transform.position, target.transform.position) > hit.distance)
                {
                    Debug.DrawLine(hit.point, transform.position, Color.green);
                    SpriteRenderer spriteRenderer;
                    if (target.TryGetComponent(out spriteRenderer))
                    {
                        GameObject targetIcon = new GameObject("TargetIcon");
                        targetIcon.transform.position = hit.point - direction * spriteRenderer.size;
                        SpriteRenderer renderer = targetIcon.AddComponent<SpriteRenderer>();
                        renderer.sprite = spriteRenderer.sprite;
                        renderer.material.color = new Color(
                            renderer.material.color.r,
                            renderer.material.color.g,
                            renderer.material.color.b,
                            0.5f
                        );

                        targetIcons.Add(targetIcon);
                    }
                }
            }
        }
    }
}
