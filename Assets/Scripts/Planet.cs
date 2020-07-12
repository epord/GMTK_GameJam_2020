using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public SpriteRenderer unvisitedSprite;
    public SpriteRenderer visitedSprite;
    public SpriteRenderer icon;

    private AudioSource repairSound;

    public bool isVisited = false;

    public GameObject planetText;

    public GameObject enemyManager;
    

    private void Start()
    {
        repairSound = GetComponent<AudioSource>();

        unvisitedSprite.enabled = true;
        visitedSprite.enabled = false;
        
        
    }

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
                unvisitedSprite.enabled = false;
                visitedSprite.enabled = true;
                repairSound.Play();
                EnemyCreator enemyCreator = enemyManager.GetComponent<EnemyCreator>();
                enemyCreator.enemyDelayMin -= 0.2f;
                enemyCreator.enemyDelayMax -= 0.2f;
                if (enemyCreator.enemyDelayMin <= 0) enemyCreator.enemyDelayMin = 0;
                if (enemyCreator.enemyDelayMax <= 0) enemyCreator.enemyDelayMax = 0;
                StartCoroutine(AddPlanetText());
            }
        }
    }

    private IEnumerator AddPlanetText()
    {
        planetText.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(10f);
        planetText.GetComponent<SpriteRenderer>().enabled = false;
    }
}
