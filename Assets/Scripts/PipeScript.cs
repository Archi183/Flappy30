using System;
using UnityEngine;

public class PipeScript : MonoBehaviour {

    [SerializeField] private float pipeMoveSpeedX = 3f;
    [SerializeField] private float pipeDestroyX = -15f;

    


    private void Update() {
        MovePipeLeft();

        if (transform.position.x < pipeDestroyX) {
            Destroy(gameObject);
        }
    }

    private void MovePipeLeft() {
    
        Vector2 pos = transform.position;
        pos.x -= pipeMoveSpeedX * Time.deltaTime;
        transform.position = pos;

    }

    public void StopMoving() {
        enabled = false;
    }

}