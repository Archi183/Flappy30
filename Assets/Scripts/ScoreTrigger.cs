using UnityEngine;

public class ScoreTrigger : MonoBehaviour {
    private bool hasScored = false;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (hasScored) return;

        if (collision.CompareTag("Player")) {
            Debug.Log("Collision triggered with player");
            hasScored = true;
            FindFirstObjectByType<GameManager>().AddScore();
        }
    }
}
