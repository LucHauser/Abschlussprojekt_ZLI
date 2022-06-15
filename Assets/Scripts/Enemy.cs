using System;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    Vector2 playerPosition;

    public int maxHealth = 100;
    public int currentHealt;
    public Image healtBar;

    public GameObject coinPreafab;
    public int coins = 1;

    private Vector3 startScale;

    private void Start() {
        currentHealt = maxHealth;
        startScale = transform.localScale;
    }

    void Update() 
    {
        playerPosition = GameObject.FindObjectOfType<PlayerScript>().transform.position;
        transform.position = Vector2.MoveTowards(transform.position, playerPosition, speed * Time.deltaTime);

        if (playerPosition.x >= transform.position.x) 
        {
            transform.localScale = new Vector3(startScale.x, startScale.y, startScale.z);
        }
        else {
            transform.localScale = new Vector3(-startScale.x, startScale.y, startScale.z);
        }
    }

    public void SendDamage(int damage) 
    {
        currentHealt -= damage;

        if (healtBar != null) {
            decimal amount = Decimal.Divide(currentHealt, maxHealth);
            healtBar.fillAmount = (float)amount;
        }

        if (currentHealt <= 0) 
        {
            try 
            {
                FindObjectOfType<QuestManager>().AddKill();
            }
            catch 
            {

                Debug.Log("No Levels in game.");
            }

            for (int i = 0; i < coins; i++) {
                
                GameObject coin = Instantiate(coinPreafab, transform.position, transform.rotation);
                try 
                {
                    FindObjectOfType<Money>().AddMoney();
                }
                catch {

                    Debug.Log("No money in Level.");
                }
            }

            Destroy(gameObject);
        }
    }
}
