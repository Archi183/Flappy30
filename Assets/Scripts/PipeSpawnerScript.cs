using UnityEngine;

public class PipeSpawnerScript : MonoBehaviour {

    [SerializeField] private GameObject pipePair;
    [SerializeField] private float spawnedInterval = 1.5f;
    [SerializeField] private float minSpawnInterval = .7f;
    [SerializeField] private float maxSpawnInterval = 2f;
    [SerializeField] private float minSpawnDisY = -1.8f;
    [SerializeField] private float maxSpawnDisY = 1.8f;
    private float spawnTimer;
    private float randomValueX;
    private float randomValueY;

    private void Start() {
        spawnTimer = spawnedInterval;    
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

    public void StopSpawning() {
        enabled = false;
    }

}
