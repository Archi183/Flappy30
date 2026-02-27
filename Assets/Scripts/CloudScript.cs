using UnityEngine;

public class CloudScript : MonoBehaviour {

    [SerializeField] private float cloudMoveSpeedX = 2f;
    [SerializeField] private float cloudDestroyX = -15f;

    


    private void Update() {
        MovePipeLeft();

        if (transform.position.x < cloudDestroyX) {
            Destroy(gameObject);
        }
    }

    private void MovePipeLeft() {
    
        Vector2 pos = transform.position;
        pos.x -= cloudMoveSpeedX * Time.deltaTime;
        transform.position = pos;

    }

    public void StopMoving() {
        enabled = false;
    }

}