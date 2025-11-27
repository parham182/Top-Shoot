using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPrefab;            // پریفب تیر
    public Transform playerTransform;
    public Transform playerTransform2;          // محل شروع تیر از روی پلیر
    public float bulletSpeed = 10f;             // سرعت تیر
    public float fireRate = 0.2f;               // فاصله زمانی بین هر شلیک (ثانیه)

    private bool isShooting = false;             // آیا دکمه نگه داشته شده؟
    private float nextFireTime = 0f;             // زمان بعدی که اجازه شلیک داریم

    PlayerHeal playerHeal;
    void Start()
    {
        playerHeal = FindAnyObjectByType<PlayerHeal>();
    }
    // این متد را از Event دکمه UI در OnPointerDown فراخوانی کنید
    public void StartShooting()
    {
        isShooting = true;
    }

    // این متد را از Event دکمه UI در OnPointerUp فراخوانی کنید
    public void StopShooting()
    {
        isShooting = false;
    }

    void Update()
    {
        if (!playerHeal.isalive) return;
        if (isShooting && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, playerTransform.position, bulletPrefab.transform.rotation);
        GameObject bullet2 = Instantiate(bulletPrefab, playerTransform2.position, bulletPrefab.transform.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Rigidbody2D rb1 = bullet2.GetComponent<Rigidbody2D>();
        if (rb != null && rb1 != null)
        {
            rb.linearVelocity = Vector2.right * bulletSpeed;
            rb1.linearVelocity = Vector2.right * bulletSpeed; // تیر به جلو پرتاب می‌شود
             // تیر به جلو پرتاب می‌شود
        }
    }
}
