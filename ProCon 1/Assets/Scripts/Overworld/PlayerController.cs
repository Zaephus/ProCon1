using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    Rigidbody2D body;
    public Camera playerCamera;
    public PlayerUnit unit;

    float horizontal;
    float vertical;
    float moveLimiter = 0.7f;

    public float runSpeed = 20.0f;

    public void Awake() {
        this.transform.position = new Vector3(unit.lastPosX,unit.lastPosY,0);
    }

    void Start() {
        body = GetComponent<Rigidbody2D>();
    }

    void Update() {

        // Gives a value between -1 and 1
        horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
        vertical = Input.GetAxisRaw("Vertical"); // -1 is down

        unit.lastPosX = this.transform.position.x;
        unit.lastPosY = this.transform.position.y;

    }

    void FixedUpdate() {

        if (horizontal != 0 && vertical != 0) { // Check for diagonal movement

            // limit movement speed diagonally, so you move at 70% speed
            horizontal *= moveLimiter;
            vertical *= moveLimiter;

        }

        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);

        playerCamera.transform.position = new Vector3(transform.position.x,transform.position.y,playerCamera.transform.position.z);
    
    }

}