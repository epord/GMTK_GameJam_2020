using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;

public class EnemyCreator : MonoBehaviour
{


    public EnemyClass[] Enemies;
    public float enemyDelayMin = 1.0f;
    public float enemyDelayMax = 3.0f;
    public float spawnDistance = 10f;
    public float probEnemyInFront = 0.4f;

    public GameObject player;

    private bool spawingEnemy = false;



    private IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(Random.Range(enemyDelayMin, enemyDelayMax));
        foreach (EnemyClass enemy in Enemies)
        {
            if (Random.RandomRange(0.0f, 1.0f) < enemy.spawnProbability)
            {
                SpawnEnemy(enemy.enemyPrefab);
            }
        }
        spawingEnemy = false;
    }

    private void SpawnEnemy(GameObject enemyPrefab)
    {
        
        Vector2 randomDirection;
        if (Random.RandomRange(0.0f, 1.0f) < probEnemyInFront)
        {
            Quaternion playerRotation = player.transform.rotation;
            randomDirection = playerRotation * Vector2.up;
        }
        else
        {
            randomDirection = Random.insideUnitCircle;
        }
        randomDirection = randomDirection.normalized * spawnDistance;
        
        UnityEngine.Vector3 finalPosition = player.transform.position + (UnityEngine.Vector3)randomDirection;
        Enemy enemy = Instantiate(enemyPrefab, finalPosition, Quaternion.identity).GetComponent<Enemy>();
        enemy.target = player;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!spawingEnemy)
        {
            spawingEnemy = true;
            StartCoroutine(SpawnEnemy());
        }
    }
}

[System.Serializable]
public class EnemyClass
{  
    public float spawnProbability;
    public float waveProbability;
    public GameObject enemyPrefab;
}