using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Controller : MonoBehaviour,IInteractable {

    private FSM fsm;

    public void Start() {

        fsm = new FSM(typeof(IdleState),GetComponents<BaseState>());

    }

    public void Update() {

        fsm.OnUpdate();

    }

    public void Interact() {

        fsm.SwitchState(typeof(InteractingState));

    }
    
}