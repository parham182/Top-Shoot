using UnityEngine;

public class GroundMove : MonoBehaviour
{
    public float speed = 2f;
    public float xDestroyLimit = -10f;
    private GroundSpown spawner;

    public void SetSpawner(GroundSpown s)
    {
        spawner = s;
    }

    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
        if (transform.position.x < xDestroyLimit)
        {
            if (spawner != null)
            {
                spawner.RemoveFromList(this.gameObject);
                spawner.SpawnAtEnd();
            }
            Destroy(gameObject);
        }
    }
}
