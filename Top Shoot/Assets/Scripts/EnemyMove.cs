using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] List<Transform> spawnPoints = new List<Transform>();
    [SerializeField] List<Transform> movePoint = new List<Transform>();
    [SerializeField] GameObject enemyPrefab;
    List<GameObject> enemy = new List<GameObject>();
    int randomSpawn;
    List<int> spawnIndex = new List<int>();
    public float duration = 2;
    float t = 0;

    void Start()
    {
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            randomSpawn = Random.Range(0, spawnPoints.Count);
            spawnIndex.Add(randomSpawn);

            enemy.Add(Instantiate(enemyPrefab, spawnPoints[i].position, enemyPrefab.transform.rotation));
        }
    }

    void Update()
    {
        if (t < 1f)
        {
            for (int i = 0; i < spawnPoints.Count; i++)
            {
                float randomdoration = Random.Range(2, duration);
                t += Time.deltaTime / randomdoration;
                enemy[i].transform.position = Vector2.Lerp(spawnPoints[spawnIndex[i]].position, movePoint[spawnIndex[i]].position, t);
            }
        }
    }
}
