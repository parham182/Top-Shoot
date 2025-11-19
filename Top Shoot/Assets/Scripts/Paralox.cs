using UnityEngine;

public class Paralox : MonoBehaviour
{
    public float moveSpeed = -2f;
    public Camera mainCamera;

    [Tooltip("نقاط اسپاون مجاز. اگر خالی باشد، از روش قدیمی استفاده می‌کند.")]
    public Transform[] respawnPoints;

    [Tooltip("محدوده تصادفی اسپاون نسبت به هر RespawnPoint محور X")]
    public Vector2 spawnXRange = new Vector2(-0.5f, 0.5f);

    [Tooltip("محدوده تصادفی اسپاون نسبت به هر RespawnPoint محور Y")]
    public Vector2 spawnYRange = new Vector2(-0.2f, 0.2f);

    [Tooltip("اگر فعال باشد، سایز تایل هنگام ری‌اسپاون به صورت تصادفی تنظیم می‌شود")]
    public bool randomizeScale = false;

    [Tooltip("محدوده تصادفی سازی Scale تایل")]
    public Vector2 scaleRange = new Vector2(0.8f, 1.3f);


    private Transform[] tiles;
    private float tileWidth;
    private int leftIndex;
    private int rightIndex;

    void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        int count = transform.childCount;
        if (count == 0)
        {
            Debug.LogError("Paralox: No child tiles found!");
            enabled = false;
            return;
        }

        tiles = new Transform[count];
        for (int i = 0; i < count; i++)
            tiles[i] = transform.GetChild(i);

        SpriteRenderer sr = tiles[0].GetComponent<SpriteRenderer>();
        tileWidth = sr.bounds.size.x;

        System.Array.Sort(tiles, (a, b) => a.position.x.CompareTo(b.position.x));

        leftIndex = 0;
        rightIndex = tiles.Length - 1;
    }

    void Update()
    {
        Vector3 movement = new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);
        foreach (var t in tiles)
            t.position += movement;

        Vector3 camPos = mainCamera.transform.position;
        float camHalfWidth = mainCamera.orthographicSize * mainCamera.aspect;
        float leftViewEdge = camPos.x - camHalfWidth;
        float rightViewEdge = camPos.x + camHalfWidth;

        // Left side
        if (tiles[leftIndex].position.x + tileWidth < leftViewEdge)
        {
            RespawnTile(leftIndex);
            rightIndex = leftIndex;
            leftIndex = (leftIndex + 1) % tiles.Length;
        }

        // Right side
        if (tiles[rightIndex].position.x - tileWidth > rightViewEdge)
        {
            RespawnTile(rightIndex);
            leftIndex = rightIndex;
            rightIndex = (rightIndex - 1 + tiles.Length) % tiles.Length;
        }
    }

    // ----------------------------------------------
    // ری‌اسپاون تمیز با چند RespawnPoint
    // ----------------------------------------------
    void RespawnTile(int index)
    {
        // --- حالت 1: استفاده از RespawnPoints ---
        if (respawnPoints != null && respawnPoints.Length > 0)
        {
            Transform p = respawnPoints[Random.Range(0, respawnPoints.Length)];

            float newX = p.position.x + Random.Range(spawnXRange.x, spawnXRange.y);
            float newY = p.position.y + Random.Range(spawnYRange.x, spawnYRange.y);

            tiles[index].position = new Vector3(newX, newY, tiles[index].position.z);
        }
        else
        {
            // --- حالت 2: روش قدیمی ---
            float newXpos;

            if (index == leftIndex)
                newXpos = tiles[rightIndex].position.x + tileWidth;
            else
                newXpos = tiles[leftIndex].position.x - tileWidth;

            tiles[index].position = new Vector3(newXpos, tiles[index].position.y, tiles[index].position.z);
        }

        // --- ویژگی جدید: تغییر سایز رندوم ---
        if (randomizeScale)
        {
            float s = Random.Range(scaleRange.x, scaleRange.y);
            tiles[index].localScale = new Vector3(s, s, tiles[index].localScale.z);
        }
    }
}
