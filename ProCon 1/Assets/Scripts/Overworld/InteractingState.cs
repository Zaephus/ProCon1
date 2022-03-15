using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractingState : BaseState {

    public float waitDuration = 3;
    private float timer = 0;

    public override void OnEnter() {

        Debug.Log("Hello World!!");
        LevelLoader.instance.LoadLevel("BattleScene");

    }

    public override void OnUpdate() {

        timer -= Time.deltaTime;
        if(timer <= 0) {
            owner.SwitchState(typeof(IdleState));
        }

    }

    public override void OnExit() {}
    
}