using UnityEngine;
using TMPro;
using System.Collections.Generic;
using Cinemachine;

public class ShopScript : MonoBehaviour
{
    public Weapon[] dataset;
    public List<int> boughtArrey = new List<int>();
    public List<int> currentArrey = new List<int>();

    public GameObject uiItemPrefab;
    public Transform itemsParent;
    public GameObject NotEnoughPanel;

    private int firstTime;

    public List<WeaponUIItem> uiItems = new List<WeaponUIItem>();

    public bool dontHaveFistElementFromStart;

    public bool isMenu;


    void Awake()
    {
        firstTime = PlayerPrefs.GetInt("f");

        if (firstTime == 0) {
            ResetAll();
            PlayerPrefs.SetInt("f", 1);
        }

        SetLenghtOfLists();
        UpdateArreys();

        if (isMenu) 
        {
            CreateUI();
        }
        else 
        {
            for (int i = 0; i < dataset.Length; i++) 
            {
                if (currentArrey[i] == 2) 
                {
                    string name = dataset[i].name;
                    GameObject item = Instantiate(dataset[i].prefab, dataset[i].prefab.transform.position, dataset[i].prefab.transform.rotation);
                    item.name = name;
                    CinemachineVirtualCamera cam = FindObjectOfType<CinemachineVirtualCamera>();
                    cam.LookAt = item.transform;
                    cam.Follow = item.transform;
                }
            }
        }
    }

    public void CreateUI() 
    {
        UpdateArreys();

        itemsParent.GetComponent<RectTransform>().sizeDelta = new Vector2(1250, (dataset.Length) * (380 + 12));

        for (int i = 0; i < dataset.Length; i++)
        {
            string name = "item" + i;
            GameObject item = Instantiate(uiItemPrefab, uiItemPrefab.transform.position, uiItemPrefab.transform.rotation);
            item.transform.SetParent(itemsParent);
            item.transform.localScale = new Vector3(1, 1, 1);
            uiItems.Add(item.GetComponent<WeaponUIItem>());
            item.name = name;

            uiItems[i].nameText.text = dataset[i].name;
            uiItems[i].priceText.text = dataset[i].price.ToString() + "$";
            uiItems[i].img.sprite = dataset[i].img;

            int x = new int();
            x = i;
            uiItems[i].button.onClick.AddListener(delegate { Buy(x); });
        }

        UpdateUI();
    }

    void SetLenghtOfLists() 
    {
        for (int i = 0; i < dataset.Length; i++) {
            boughtArrey.Add(0);
            currentArrey.Add(0);
        }
    }

    public void Buy (int index)
    {
        if (boughtArrey[index] == 2)
        {
            ResetCurrentArrey();
            PlayerPrefs.SetInt("c" + index, 2);

            UpdateArreys();
        }
        else
        {
            int money = FindObjectOfType<Money>().money;

            if (dataset[index].price <= money)
            {
                money -= dataset[index].price;
                FindObjectOfType<Money>().SaveMoney();

                PlayerPrefs.SetInt("b" + index, 2);

                ResetCurrentArrey();
                PlayerPrefs.SetInt("c" + index, 2);
                UpdateArreys();
            }
            else
            {
               NotEnoughPanel.SetActive(true);
            }
        }

        UpdateUI();
    }

    void UpdateArreys ()
    {
        for (int i = 0; i < dataset.Length; i++)
        {
            boughtArrey[i] = PlayerPrefs.GetInt("b" + i);
        }

        for (int i = 0; i < dataset.Length; i++)
        {
            currentArrey[i] = PlayerPrefs.GetInt("c" + i);
        }
    }

    void ResetCurrentArrey ()
    {
        for (int i = 0; i < dataset.Length; i++)
        {
            PlayerPrefs.SetInt("c" + i, 1);
        }
    }

    void ResetAll()
    {
        for (int i = 0; i < dataset.Length; i++)
        {
            PlayerPrefs.SetInt("b" + i, 1);
            PlayerPrefs.SetInt("c" + i, 1);
        }

        if (!dontHaveFistElementFromStart) 
        {
            PlayerPrefs.SetInt("b0", 2);
            PlayerPrefs.SetInt("c0", 2);
        }
    }

    public void UpdateUI ()
    {
        for (int i = 0; i < dataset.Length; i++)
        {
            if (boughtArrey[i] == 2)
            {
                uiItems[i].button.GetComponentInChildren<TextMeshProUGUI>().text = "Bought";
                uiItems[i].priceText.text = "";
            }
            else
            {
                uiItems[i].button.GetComponentInChildren<TextMeshProUGUI>().text = "Buy";
            }

            if (currentArrey[i] == 2)
            {
                uiItems[i].button.GetComponentInChildren<TextMeshProUGUI>().text = "Current";
            }
        }
    }

    public void NotEnough()
    {
        NotEnoughPanel.SetActive(false);
    }
}
