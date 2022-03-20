using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractingState : BaseState {

    public float waitDuration = 3;
    private float timer = 0;

    public override void OnEnter() {

        timer = waitDuration;

        LevelLoader.instance.LoadLevel("DialogueScene");

    }

    public override void OnUpdate() {

        timer -= Time.deltaTime;
        if(timer <= 0) {
            owner.SwitchState(typeof(IdleState));
        }

    }

    public override void OnExit() {}
    
}