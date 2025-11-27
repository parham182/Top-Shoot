using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] Transform spawnPoints;
    [SerializeField] Transform movePoint;
    [SerializeField] GameObject enemyPrefab;
    GameObject enemy;
    float time = 0f;

    public bool enemyspawned = false;
    public float duration = 5f;
    float t = 0;

    void Start()
    {



    }

    void Update()
    {

        if (!enemyspawned) time += Time.deltaTime;
        if (time >= 3f)
        {
            enemy = Instantiate(enemyPrefab, spawnPoints.position, enemyPrefab.transform.rotation);
            time = 0;
            enemyspawned = true;
        }

        if (!enemyspawned) return;

        float randomdoration = Random.Range(2, duration);
        t += Time.deltaTime / randomdoration;
        enemy.transform.position = Vector2.Lerp(spawnPoints.position, movePoint.position, t);

    }
}
