using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour {

    public event EventHandler Jump;

    public PlayerInputActionMap playerInputActionMap { get; private set; }

    private void Awake() {
    
        playerInputActionMap = new PlayerInputActionMap();
    }

    private void OnEnable() {
    
        playerInputActionMap.Enable();
        playerInputActionMap.Player.Jump.performed += JumpPerformed;
    }

    private void OnDisable() {

        playerInputActionMap.Player.Jump.performed -= JumpPerformed;
        playerInputActionMap.Disable();
    }

    private void JumpPerformed(InputAction.CallbackContext context) {

        Jump?.Invoke(this, EventArgs.Empty);
    }
}