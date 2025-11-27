using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] float enemyHeal = 6;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject exploadParticel;
    [SerializeField] Transform cannon1;
    [SerializeField] Transform cannon2;
    [SerializeField] float bulletSpeed = 10f;
    public float enemyHealnow = 0;
    EnemyMove enemyMove;
    float timer = 0f;
    void Start()
    {
        enemyMove = FindFirstObjectByType<EnemyMove>();
        enemyHealnow = enemyHeal;
    }

    
    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= 3f)
        {
            enemyShoot();
            timer = 0f;
        }

        healHandeller();
    }

    private void healHandeller()
    {
        if (enemyHealnow <= 0)
        {
            Instantiate(exploadParticel, this.gameObject.transform.position, quaternion.identity);
            Destroy(this.gameObject);
            enemyMove.enemyspawned = false;
        }
    }

    void enemyShoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, cannon1.position, bulletPrefab.transform.rotation);
        GameObject bullet2 = Instantiate(bulletPrefab, cannon2.position, bulletPrefab.transform.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Rigidbody2D rb2 = bullet2.GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.left * bulletSpeed;
        rb2.linearVelocity = Vector2.left * bulletSpeed;


    }
}
