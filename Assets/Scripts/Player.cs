using System;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] private GameInput gameInput;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float flapStrength = 5f;
    public event EventHandler birdHit;

    private void OnEnable() {
        gameInput.Jump += GameInput_Jump;
    }

    private void OnDisable() {
        gameInput.Jump -= GameInput_Jump;
    }

    private void GameInput_Jump(object sender, System.EventArgs e) {
        if (gameManager.gameOver) return;

        rb.linearVelocity = new Vector2(rb.linearVelocity.x, flapStrength);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        birdHit?.Invoke(this, EventArgs.Empty);
    }

}
