using UnityEngine;

public class Coin : MonoBehaviour
{
    private Rigidbody2D rb;
    public Vector2 speedRange;
    public float lifetime = 3;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * Random.Range(speedRange.x, speedRange.y));
        rb.AddForce(transform.right * Random.Range(speedRange.x, speedRange.y));
        Destroy(gameObject, lifetime);
    }
}
