using UnityEngine;

public class EnemyChild : MonoBehaviour
{
    public Enemy enemyParent;

    public void SendDamage(int damage) 
    {
        enemyParent.SendDamage(damage);
    }
}
