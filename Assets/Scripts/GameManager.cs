using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField] private GameInput gameInput;
    [SerializeField] private Player player;
    [SerializeField] private PipeSpawnerScript pipeSpawnerScript; 

    enum GameState {
        WaitingToStart,
        Playing,
        GameOver
    }

    GameState currentState = GameState.WaitingToStart;

    private void OnEnable() {
        player.birdHit += player_birdHit;
        gameInput.Jump += GameInput_Pressed_Space;
    }

    private void OnDisable() {
        player.birdHit -= player_birdHit;
        gameInput.Jump -= GameInput_Pressed_Space;
    }

    private void GameInput_Pressed_Space(object sender, System.EventArgs e) {
        if (currentState == GameState.WaitingToStart) {
            currentState = GameState.Playing;
            player.SetCanMove(true);
            player.SetSimulated(true);
            pipeSpawnerScript.StartSpawning();
            player.FirstFlap();
        }
    }

    private void player_birdHit(object sender, System.EventArgs e) {
        currentState = GameState.GameOver;
        player.SetCanMove(false);
        pipeSpawnerScript.StopSpawning();
        PipeScript[] pipes = FindObjectsByType<PipeScript>(FindObjectsSortMode.None);
        foreach (PipeScript pipe in pipes) {
            pipe.StopMoving();
        }
    }

}
