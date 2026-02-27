using UnityEngine;

public class CloudSpawnerScript : MonoBehaviour {


    [SerializeField] private GameObject could;
    [SerializeField] private float spawnedInterval = 10f;
    [SerializeField] private float minSpawnInterval = 8f;
    [SerializeField] private float maxSpawnInterval = 20f;
    [SerializeField] private float minSpawnDisY = -1.8f;
    [SerializeField] private float maxSpawnDisY = 1.8f;
    private float spawnTimer;
    private float randomValueX;
    private float randomValueY;

    private void Start() {
        enabled = false;
    }


    private void Update() {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f) {
            randomValueX = Random.Range(minSpawnInterval, maxSpawnInterval);
            randomValueY = Random.Range(minSpawnDisY, maxSpawnDisY);
            spawnedInterval = randomValueX;
            SpawnClouds();
            spawnTimer = spawnedInterval;
        }
            

    }

    private void SpawnClouds() {
        Vector2 randomSpawnPosition = new Vector2(transform.position.x, randomValueY);
        Instantiate(could, randomSpawnPosition, Quaternion.identity);
    }

    public void StopSpawningClouds() {
        enabled = false;
    }

    public void StartSpawningClouds() {
        spawnTimer = spawnedInterval;
        enabled = true;
    }


}