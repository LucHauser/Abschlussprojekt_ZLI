using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 5;
    public float lifetime = 3;

    private Rigidbody2D rb;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifetime);
    }

    void Update() {
        rb.AddForce(transform.up * speed);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag != "enemy") 
        {
            Destroy(gameObject);
        }
    }
}
