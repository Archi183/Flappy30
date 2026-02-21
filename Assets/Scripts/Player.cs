using System;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] private GameInput gameInput;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float flapStrength = 5f;
    private bool canMove = false;
    public event EventHandler birdHit;

    private void Start() {
        rb.simulated = false;
    }

    private void OnEnable() {
        gameInput.Jump += GameInput_Jump;
    }

    private void OnDisable() {
        gameInput.Jump -= GameInput_Jump;
    }

    private void GameInput_Jump(object sender, System.EventArgs e) {
        if(!canMove) return;
        Debug.Log("Current canMove state" + canMove);
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, flapStrength);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        birdHit?.Invoke(this, EventArgs.Empty);
    }

    public void SetCanMove(bool value) {
        canMove = value;
    }

    public void SetSimulated (bool value) {
        rb.simulated = value;
    }

    public void FirstFlap() {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, flapStrength);
    }


}
