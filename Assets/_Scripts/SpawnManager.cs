using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject healthPickup;
    [SerializeField] private float spawnRate = 1;
    [SerializeField] private float spawnHeight;
    [SerializeField] float leftSideOfScreen;
    [SerializeField] float rightSideOfScreen;
    private bool startSpawning;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
        //As the variable states, they find the left and right side of the phone's screen. This makes the game work no matter the screen size
        leftSideOfScreen = Camera.main.transform.position.x - Camera.main.orthographicSize * Screen.width / Screen.height;
        rightSideOfScreen = Camera.main.transform.position.x + Camera.main.orthographicSize * Screen.width / Screen.height;
    }

    private void Start()
    {
        spawnHeight = Camera.main.orthographicSize;
    }

    public void StartSpawning()
    {
        startSpawning = true;
        StartCoroutine("SpawnTimer");
    }

    public void StopSpawning()
    {
        startSpawning = false;
        StopCoroutine("SpawnTimer");
    }

    public bool GetSpawningStatus()
    {
        return startSpawning;
    }

    public IEnumerator SpawnTimer()
    {
        yield return new WaitForSeconds(spawnRate);
        SpawnObject();
        StartCoroutine("SpawnTimer");
    }

    private Vector3 Spawnpos()
    {
        return new Vector3(Random.Range(leftSideOfScreen, rightSideOfScreen), spawnHeight, 0);
    }
    public void SpawnObject()
    {
            Instantiate(enemyPrefab, Spawnpos(), Quaternion.identity);
    }  
     
    public float GetSpawnRate()
    {
        return spawnRate;
    }

    public void SetSpawnRate(float vSpawnRate)
    {
        spawnRate = vSpawnRate;
    }

}
