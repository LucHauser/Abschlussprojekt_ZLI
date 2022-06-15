using UnityEngine;
using TMPro;

public class Money : MonoBehaviour 
{
    public int money;
    public TextMeshProUGUI moneyInGameText;
    public TextMeshProUGUI moneyGameoverText;
    public TextMeshProUGUI moneyInShopText;

    void Start() 
    {
        money = PlayerPrefs.GetInt("money");
    }

    void Update() 
    {
        moneyInGameText.text = money.ToString();
    }

    public void AddMoney(int amount = 1) {
        money += amount;
    }

    public void SaveMoney() {
        PlayerPrefs.SetInt("money", money);

        if (moneyGameoverText != null)
        {
            moneyGameoverText.text = money.ToString();
        }
    }
}
