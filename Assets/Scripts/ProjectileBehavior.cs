using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    public float speed = 10f;
    public GameObject enemyHit;
    public float lifetime = 3f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyFollow enemy = other.gameObject.GetComponent<EnemyFollow>();

            if (enemy != null)
            {
                enemy.TakeHit();  
            }

            Destroy(gameObject);   
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
