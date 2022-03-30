using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractingState : BaseState {

    public float waitDuration = 3;
    private float timer = 0;

    private NPC_Controller npc;

    public override void OnEnter() {

        npc = GetComponent<NPC_Controller>();

        timer = waitDuration;

        LevelLoader.instance.StartDialogue(npc.unit);

        if(TryGetComponent(out NPC_Controller controller))
		{
            controller.unit.canSpawn = true;
		}

    }

    public override void OnUpdate() {

        timer -= Time.deltaTime;
        if(timer <= 0) {
            owner.SwitchState(typeof(IdleState));
        }

    }

    public override void OnExit() {}
    
}