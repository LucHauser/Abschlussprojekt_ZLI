using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapsUIManager : MonoBehaviour 
{
    public Map[] mapDatasets;
    public List<UIItem> uiitems = new List<UIItem>();

    public Transform mapsParent;
    public GameObject mapPrefab;

    void Start() {
        CreateUI();
    }

    public void CreateUI() 
    {
        mapsParent.GetComponent<RectTransform>().sizeDelta = new Vector2(1250, (mapDatasets.Length) * (380 + 12));

        for (int i = 0; i < mapDatasets.Length; i++) {
            string name = "map" + i;
            var mapDataset = mapDatasets[i];
            GameObject map = Instantiate(mapPrefab, mapPrefab.transform.position, mapPrefab.transform.rotation);
            map.transform.SetParent(mapsParent);
            map.transform.localScale = new Vector3(1, 1, 1);
            uiitems.Add(map.GetComponent<UIItem>());
            map.name = name;

            uiitems[i].nameText.text = mapDataset.name;
            uiitems[i].infoText.text = mapDataset.info;
            uiitems[i].img.sprite = mapDataset.img;

            int x = new int();
            x = i;
            uiitems[i].button.onClick.AddListener(delegate { SceneManager.LoadScene(mapDataset.scene); });
        }
    }

    //public void Play(int index) 
    //{
    //    Debug.Log(index);
    //    Debug.Log(mapDatasets);
    //    SceneManager.LoadScene(mapDatasets[index].scene);
    //}
}
