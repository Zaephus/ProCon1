using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Controller : MonoBehaviour,IInteractable {

    private FSM fsm;

    public EnemyUnit unit;

    public void Awake() {
        //this.transform.position = new Vector3(unit.lastPosX,unit.lastPosY,0);
        unit.currentHealth = unit.maxHealth;
    }

    public void Start() {

        fsm = new FSM(typeof(IdleState),GetComponents<BaseState>());

    }

    public void Update() {

        fsm.OnUpdate();

        // unit.lastPosX = this.transform.position.x;
        // unit.lastPosY = this.transform.position.y;

    }

    public void Interact() {

        fsm.SwitchState(typeof(InteractingState));

    }
    
}