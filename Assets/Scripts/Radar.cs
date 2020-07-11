using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
    public Planet[] targets;
    public float activationTime = 3.0f; // time in seconds of how long the radar will be active
    public float cooldown = 30.0f; // time in seconds the player has to wait before using the radar again
    public bool isActive = true;

    [HideInInspector]
    public float deviation = 0.0f;

    private List<GameObject> targetIcons = new List<GameObject>();
    private bool canActivate = true;
    private float driftAngle = 0.0f;

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

        foreach (Planet target in targets)
        {
            if (target.isVisited) continue; // Do not show visited planets

            Vector2 direction = target.transform.position - transform.position;
            driftAngle = Mathf.Cos(Time.time / 30.0f) * deviation * 2.0f * Mathf.PI / 360.0f;
            float finalAngle = Mathf.Atan2(direction.y, direction.x) + driftAngle;
            direction = new Vector2(Mathf.Cos(finalAngle), Mathf.Sin(finalAngle));
            direction.Normalize();
            int cameraLayer = 1 << 8;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, Mathf.Infinity, cameraLayer);
            if (hit.collider != null)
            {
                if (Vector2.Distance(transform.position, target.transform.position) > hit.distance)
                {
                    Debug.DrawLine(hit.point, transform.position, Color.green);
                    SpriteRenderer spriteRenderer;
                    if (target.TryGetComponent(out spriteRenderer))
                    {
                        GameObject targetIcon = new GameObject("TargetIcon");
                        Vector3 position = hit.point;
                        position.z = -2;
                        targetIcon.transform.position = position;
                        SpriteRenderer renderer = targetIcon.AddComponent<SpriteRenderer>();
                        renderer.sprite = spriteRenderer.sprite;
                        renderer.material.color = new Color(
                            renderer.material.color.r,
                            renderer.material.color.g,
                            renderer.material.color.b,
                            0.5f
                        );
                        targetIcon.transform.localScale = new Vector3(0.03f, 0.03f, 0.03f);

                        targetIcons.Add(targetIcon);
                    }
                }
            }
        }
    }
}
