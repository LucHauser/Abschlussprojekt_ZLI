using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverPanel;
    public GameObject wonPanel;

    private void Start() 
    {
        Time.timeScale = 1;
    }


    private void SaveAndDisable() 
    {
        try
        {
            FindObjectOfType<Money>().SaveMoney();
        }
        catch 
        {

            Debug.Log("Map has no money");
        }

        try 
        {
            FindObjectOfType<QuestManager>().Save();
        }
        catch 
        {

            Debug.Log("Has no Quests");
        }

        FindObjectOfType<PlayerScript>().enabled = false;
    }

    public void GameEnd() 
    {
        SaveAndDisable();
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
    }

    public void Won() 
    {
        SaveAndDisable();
        Time.timeScale = 0;
        wonPanel.SetActive(true);
    }

    public void Restart() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMaps() 
    {
        SceneManager.LoadScene("Maps");
    }

    public void LoadMenu() 
    {
        SceneManager.LoadScene("Menu");
    }

    public void Shop()
    {
        SceneManager.LoadScene("Shop");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
