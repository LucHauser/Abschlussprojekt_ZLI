using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    public List<GameObject> prefabs;
    public Transform[] spawnPoints;
    public Vector2 delayRange = new Vector2(2,6);

    public bool getsFaster;
    public float stepSize = 0.001f;
    public Vector2 minSpace = new Vector2(1,2);
    public float delay = 1;

    public bool getsHarder;
    public GameObject secondPrefab;
    public float delayOfEnemy = 20;

    void Start()
    {
        StartCoroutine(SpawnPrefab());

        if (getsFaster)
        {
            StartCoroutine(SpeedUp());
        }

        if (getsHarder) 
        {
            StartCoroutine(Harder());
        }
    }

    IEnumerator SpawnPrefab() 
    {
        float randomDelay = Random.Range(delayRange.x, delayRange.y);
        yield return new WaitForSeconds(randomDelay);
        int randomIndex = Random.Range(0, spawnPoints.Length);
        int randomPrefab = Random.Range(0, prefabs.Count);
        GameObject prefabClone = Instantiate(prefabs[randomPrefab], spawnPoints[randomIndex].position, spawnPoints[randomIndex].rotation);
        prefabClone.name = prefabs[randomPrefab].name;
        StartCoroutine(SpawnPrefab());
    }

    IEnumerator SpeedUp() 
    {
        if (delayRange.x > minSpace.x) {
            delayRange.x -= stepSize;
        }

        if (delayRange.y > minSpace.y) {
            delayRange.y -= stepSize;
        }

        yield return new WaitForSeconds(delay);
        StartCoroutine(SpeedUp());
    }

    IEnumerator Harder() 
    {
        yield return new WaitForSeconds(delayOfEnemy);
        prefabs.Add(secondPrefab);
    }
}
