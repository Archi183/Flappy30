using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] private GameInput gameInput;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float flapStrength = 5f;
    [SerializeField] private Sprite wingUp;
    [SerializeField] private Sprite wingDown;
    [SerializeField] private Sprite wingNeutral;

    private SpriteRenderer spriteRenderer;
    private bool canMove = false;
    public event EventHandler birdHit;

    private IEnumerator FlapAnimation() {

        spriteRenderer.sprite = wingUp;

        yield return new WaitForSeconds(0.07f);

        spriteRenderer.sprite = wingDown;

        yield return new WaitForSeconds(0.15f);

        spriteRenderer.sprite = wingNeutral;
    }

    private void Start() {
        rb.simulated = false;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
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
        StartCoroutine(FlapAnimation());
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
        StartCoroutine(FlapAnimation());
    }


}
