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
    [SerializeField] private CloudSpawnerScript cloudSpawnerScript; 
    [SerializeField] private AudioManager audioManager;

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
            pipeSpawnerScript.StartSpawningPipes();
            cloudSpawnerScript.StartSpawningClouds();
            player.FirstFlap();
            audioManager.PlayFlap();
            audioManager.StartGameplayMusicCycle();
        } else if (currentState == GameState.Playing) {
            audioManager.PlayFlap();
        }
    }

    private void player_birdHit(object sender, System.EventArgs e) {
        if (currentState == GameState.Playing) {
            audioManager.StopGameplayMusicCycle();
            audioManager.PlayGameOver();
        }
        currentState = GameState.GameOver;
        player.SetCanMove(false);
        pipeSpawnerScript.StopSpawningPipes();
        cloudSpawnerScript.StopSpawningClouds();
        PipeScript[] pipes = FindObjectsByType<PipeScript>(FindObjectsSortMode.None);
        foreach (PipeScript pipe in pipes) {
            pipe.StopMoving();
        }
        CloudScript[] clouds = FindObjectsByType<CloudScript>(FindObjectsSortMode.None);
        foreach (CloudScript cloud in clouds) {
            cloud.StopMoving();
        }
        gameOverPanel.SetActive(true);
        audioManager.PlayHit();
    }

    public void AddScore() {
        score ++;
        scoreText.text = score.ToString();
        audioManager.PlayScore();

        AdjustDifficulty();
    }

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        audioManager.PlayOptionSelect();
    }

    private void AdjustDifficulty() {

    float difficulty = score * 0.005f;

    float minInterval = Mathf.Clamp(1.5f - difficulty, 0.5f, 1.5f);
    float maxInterval = Mathf.Clamp(2.0f - difficulty, 0.8f, 2.0f);
    pipeSpawnerScript.SetSpawnInterval(minInterval, maxInterval);

    float heightRange = Mathf.Clamp(score * 0.06f, 0.5f, 1.8f);
    pipeSpawnerScript.SetSpawnHeight(-heightRange, heightRange);   
    }

    public void GameExit() {
        SceneManager.LoadScene("MainMenu");
    }

}
