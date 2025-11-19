using UnityEngine;
using System.Collections.Generic;

public class GroundSpown : MonoBehaviour
{
    public GameObject objectPrefab;
    public int initialCount = 10;
    public float startX = 10f; // جای اولین آبجکت
    public float ySpawn = 0f;
    public float distance = 22f; // فاصله هر آبجکت با بعدی

    private List<GameObject> objectList = new List<GameObject>();

    void Start()
    {
        // اسپاون اولیه آبجکت‌ها
        for (int i = 0; i < initialCount; i++)
        {
            Vector3 spawnPos = new Vector3(startX + i * distance, ySpawn, 1);
            GameObject obj = Instantiate(objectPrefab, spawnPos, Quaternion.identity);
            obj.GetComponent<GroundMove>().SetSpawner(this);
            objectList.Add(obj);
        }
    }

    // این متد وقتی آبجکت حذف شد صدا زده میشه
    public void SpawnAtEnd()
    {
        float lastX = startX;
        if (objectList.Count > 0)
            lastX = objectList[objectList.Count - 1].transform.position.x;

        Vector3 spawnPos = new Vector3(lastX + distance, ySpawn, 0);
        GameObject obj = Instantiate(objectPrefab, spawnPos, Quaternion.identity);
        obj.GetComponent<GroundMove>().SetSpawner(this);
        objectList.Add(obj);
    }

    // حذف از لیست با توجه به ریفرنس
    public void RemoveFromList(GameObject obj)
    {
        objectList.Remove(obj);
    }
}
