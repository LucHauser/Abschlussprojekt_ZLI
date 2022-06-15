using UnityEngine;
using TMPro;

public class GameOverInMulitplayer : MonoBehaviour
{
    public GameObject gameOverPanel;
    public TextMeshProUGUI resultText;
    public TextMeshProUGUI countDownText;

    public void GameOver(bool won) 
    {
        gameOverPanel.SetActive(true);

        if (won) 
        {  
            resultText.text = "You have won";   
        }
        else 
        {
            resultText.text = "You have lost";
        }
    }

    public void SetSecondsLeft(int seconds) 
    {
        countDownText.text = "Room closes in " + seconds + "  seconds";
    }
}
