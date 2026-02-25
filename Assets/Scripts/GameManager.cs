using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour {
    [SerializeField] private GameObject startText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private Player player;
    [SerializeField] private PipeSpawnerScript pipeSpawnerScript; 

    private int score=0;



    enum GameState {
        WaitingToStart,
        Playing,
        GameOver
    }

    GameState currentState = GameState.WaitingToStart;

    private void Start() {
        startText.SetActive(true);
        gameOverPanel.SetActive(false);
        scoreText.text = "0";
    }

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
            startText.SetActive(false);
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
        gameOverPanel.SetActive(true);
    }

    public void AddScore() {
        score ++;
        scoreText.text = score.ToString();
    }

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
