using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private bool initDone = false;

    public Rigidbody2D body;
    public Camera playerCamera;
    public PlayerUnit unit;

    float horizontal;
    float vertical;
    float moveLimiter = 0.7f;

    public float runSpeed = 20.0f;

    void Start() {
        
        SaveSystem.instance.LoadUnit(unit,unit.name);

        if(initDone == false) {
            initDone = true;
            unit.lastPosX = unit.startPosX;
            unit.lastPosY = unit.startPosY;
        }

        this.transform.position = new Vector2(unit.lastPosX,unit.lastPosY);
        
        unit.currentHealth = unit.maxHealth;
        unit.currentAttack = unit.baseAttack;
        unit.currentDefense = unit.baseDefense;

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