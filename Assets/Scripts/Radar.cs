using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
    public Planet[] targets;
    public float activationTime = 3.0f; // time in seconds of how long the radar will be active
    public float cooldown = 30.0f; // time in seconds the player has to wait before using the radar again
    public bool isActive = true;

    public SpriteRenderer arrow;

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

    //private void Update()
    //{
        // Uncomment to use a key to activate radar
        //if (canActivate && Input.GetButtonDown("Radar"))
        //{
        //    StartCoroutine(ActivateRadar());
        //}
    //}

    private void Update()
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

                    float distance = Vector2.Distance(transform.position, target.transform.position);
                    float scale = 0.2f - distance / 350.0f * 0.18f;

                    // Add icon
                    GameObject targetIcon = new GameObject("TargetIcon");

                    Vector2 dir = hit.point - new Vector2(transform.position.x, transform.position.y);
                    dir.Normalize();
                    Vector3 position = hit.point;
                    position.z = -5;
                    targetIcon.transform.position = position - new Vector3(dir.x, dir.y, 0) * 0.7f;

                    targetIcon.transform.localScale = new Vector3(1, 1, 1) * scale;

                    SpriteRenderer sr = targetIcon.AddComponent<SpriteRenderer>();
                    sr.sprite = target.icon.sprite;
                    sr.material = target.icon.material;


                    // Add arrow
                    GameObject arrowIcon = new GameObject("Arrow");

                    float rotation = Vector2.SignedAngle(
                        Vector2.up,
                        hit.point - new Vector2(transform.position.x, transform.position.y)
                    );
                    arrowIcon.transform.rotation = Quaternion.Euler(new Vector3(0, 0, rotation));
                    arrowIcon.transform.position = hit.point;
                    arrowIcon.transform.localScale = new Vector3(1, 1, 1) * scale;

                    SpriteRenderer sr2 = arrowIcon.AddComponent<SpriteRenderer>();
                    sr2.sprite = arrow.sprite;
                    sr2.material = arrow.sharedMaterial;


                    // Add icon and arrow
                    targetIcons.Add(targetIcon);
                    targetIcons.Add(arrowIcon);
                }
            }
        }
    }
}
