using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField]
    private int enemyNumber;
    [SerializeField]
    private List<GameObject> enemyObject;
    [SerializeField]
    private float radius;

    private Coroutine spawnCoroutine;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && spawnCoroutine == null)
            spawnCoroutine = StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        InstantiateEnemy();
        yield return new WaitForSeconds(1.5f);

        if (GameObject.FindGameObjectsWithTag("Enemy").Length >= enemyNumber)
        {
            yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Enemy").Length < enemyNumber);
            spawnCoroutine = StartCoroutine(SpawnEnemies());
        } 
        else
            spawnCoroutine = StartCoroutine(SpawnEnemies());
    }

    private void InstantiateEnemy()
    {
        float xPos, zPos;
        xPos = Random.Range(transform.position.x - (radius / 2), transform.position.x + (radius / 2));
        zPos = Random.Range(transform.position.z - (radius / 2), transform.position.z + (radius / 2));
        //Vector3 spawnLocation = RandomNavMeshLocation();
        Instantiate(enemyObject[Random.Range(0, enemyObject.Count - 1)], new Vector3(xPos, 0, zPos), Quaternion.identity); 
    }
}
