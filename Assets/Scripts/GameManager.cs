using UnityEngine;

public class GameManager : MonoBehaviour {
    [SerializeField] private Player player;
    [SerializeField] private PipeSpawnerScript pipeSpawnerScript; 

    public bool gameOver = false;

    private void OnEnable() {
        player.birdHit += player_birdHit;
    }

    private void OnDisable() {
        player.birdHit -= player_birdHit;
    }

    private void player_birdHit(object sender, System.EventArgs e) {
        gameOver = true;
        pipeSpawnerScript.StopSpawning();
        PipeScript[] pipes = FindObjectsByType<PipeScript>(FindObjectsSortMode.None);
        foreach (PipeScript pipe in pipes) {
            pipe.StopMoving();
        }
    }

}
