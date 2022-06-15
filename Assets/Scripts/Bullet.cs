using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;

    void OnTriggerEnter2D(Collider2D collision) 
    {
        try 
        {
            if (collision.tag == "enemy") 
            {
                collision.GetComponent<Enemy>().SendDamage(damage);
            }
            else if (collision.tag == "enemy_child") 
            {
                collision.GetComponent<EnemyChild>().SendDamage(damage);
            }
        }
        catch 
        {
            Debug.Log("Enemy can not die.");
        }

        if (collision.tag != "Player") 
        {
            Destroy(gameObject);
        }
    }
}
