using UnityEngine;

public class PipeSpawnerScript : MonoBehaviour {

    [SerializeField] private GameObject pipePair;
    private float spawnedInterval = 1.5f;
    private float minSpawnInterval = .7f;
    private float maxSpawnInterval = 2f;
    private float minSpawnDisY = -1.8f;
    private float maxSpawnDisY = 1.8f;
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
            SpawnPipe();
            spawnTimer = spawnedInterval;
        }
            

    }

    private void SpawnPipe() {
        Vector2 randomSpawnPosition = new Vector2(transform.position.x, randomValueY);
        Instantiate(pipePair, randomSpawnPosition, Quaternion.identity);
    }

    public void StopSpawningPipes() {
        enabled = false;
    }

    public void StartSpawningPipes() {
        spawnTimer = spawnedInterval;
        enabled = true;
    }

    public void SetSpawnInterval(float min, float max) {
    minSpawnInterval = min;
    maxSpawnInterval = max;
    }

    public void SetSpawnHeight(float minY, float maxY) {
    minSpawnDisY = minY;
    maxSpawnDisY = maxY;
}
}
