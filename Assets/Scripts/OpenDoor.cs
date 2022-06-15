using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public GameObject coinPreafab;
    public int coins;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") 
        {
            FindObjectOfType<QuestManager>().OpenDoor();

            for (int i = 0; i < coins; i++) {
                FindObjectOfType<QuestManager>().AddKill();
                GameObject coin = Instantiate(coinPreafab, transform.position, transform.rotation);
            }

            Destroy(gameObject);
        }
    }
}
