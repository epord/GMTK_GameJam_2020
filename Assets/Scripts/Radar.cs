using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
    public GameObject[] targets;

    private List<GameObject> targetIcons = new List<GameObject>();

    private void FixedUpdate()
    {
        // Remove previous target icons
        foreach (GameObject targetIcon in targetIcons)
        {
            Destroy(targetIcon);
        }
        targetIcons.Clear();

        //Debug.DrawRay(transform.position, target.transform.position - transform.position);
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

                        targetIcons.Add(targetIcon);
                    }
                }
            }
        }
    }
}
