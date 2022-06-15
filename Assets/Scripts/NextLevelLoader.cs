using UnityEngine;

public class NextLevelLoader : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            FindObjectOfType<QuestManager>().CloseDoor();
            FindObjectOfType<QuestManager>().NextQuest();
            Destroy(gameObject);
        }
    }
}
