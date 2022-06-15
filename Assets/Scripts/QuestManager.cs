using UnityEngine;
using TMPro;
using Cinemachine;

public class QuestManager : MonoBehaviour
{
    public string mapname;
    public Quest[] quests;
    public GameObject[] doors;
    public Transform[] spawnPoints;
    public int currentQuest = -1;
    public TextMeshProUGUI questText;

    private int kills;

    private CinemachineVirtualCamera cam;
    private float normalZoom;

    void Start()
    {
        cam = FindObjectOfType<CinemachineVirtualCamera>();
        normalZoom = cam.m_Lens.OrthographicSize;
        LoadCurrentQuest();
    }

    private void LoadCurrentQuest() 
    {
        currentQuest = PlayerPrefs.GetInt("currentQuest" + mapname);
        questText.text = quests[currentQuest].quest;
        quests[currentQuest].objectsOfLevel.SetActive(true);

        NextLevelLoader loader = spawnPoints[currentQuest].GetComponent<NextLevelLoader>();

        if (loader != null) { Destroy(loader); }

        FindObjectOfType<PlayerScript>().transform.position = spawnPoints[currentQuest].position;
        Destroy(loader.gameObject);

        if (quests[currentQuest].changeZoom) {
            cam.m_Lens.OrthographicSize = quests[currentQuest].zoom;
        }
        else {
            cam.m_Lens.OrthographicSize = normalZoom;
        }
    }

   public void NextQuest() 
   {
        quests[currentQuest].objectsOfLevel.SetActive(false);
        currentQuest++;
        Save();

        if (currentQuest == quests.Length) {
            currentQuest = 0;
            FindObjectOfType<GameOver>().Won();
            return;
        }

        questText.text = quests[currentQuest].quest;
        quests[currentQuest].objectsOfLevel.SetActive(true);

        if (quests[currentQuest].changeZoom) {
            cam.m_Lens.OrthographicSize = quests[currentQuest].zoom;
        }
        else {
            cam.m_Lens.OrthographicSize = normalZoom;
        }
    }

    public void Save() 
    {
        PlayerPrefs.SetInt("currentQuest" + mapname, currentQuest);
    }

    void Update()
    {
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("enemy");

        if (quests[currentQuest].hasToKill && enemys.Length > quests[currentQuest].maxEnemy) 
        {
            Destroy(FindObjectOfType<PrefabSpawner>().gameObject);
        }

        if (quests[currentQuest].hasToKill && kills >= quests[currentQuest].killsForNextLevel) 
        {
            quests[currentQuest].objectsOfLevel.SetActive(false);

            foreach (var enemy in enemys) {
                Destroy(enemy);
            }

            kills = 0;

            if (currentQuest >= 0) {
                try 
                {
                    doors[currentQuest].GetComponent<Animator>().Play("door_open");
                }
                catch {
                    Debug.Log("Door not found");
                }
            }

            questText.text = "";

            if (quests[currentQuest].loadNextLevelImmediately) {
                NextQuest();
            }
        }

        if (quests[currentQuest].hasCountText) 
        {
            quests[currentQuest].countText.text = kills.ToString();
        }
    }

    public void CloseDoor() {
        try 
        {
            doors[currentQuest].GetComponent<Animator>().Play("door_close");
        }
        catch 
        {
            Debug.Log("Door not found");
        }
    }

    public void OpenDoor() {
        doors[currentQuest].GetComponent<Animator>().Play("door_open");
    }

    public void AddKill() 
    {
        kills++;
    }
}
