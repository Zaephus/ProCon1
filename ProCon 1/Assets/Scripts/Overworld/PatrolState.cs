using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : BaseState {

    public Transform npc;

    public float stoppingDist = 2;

    public float maxSpeed = 3;

    public bool canWalk = true;

    public Transform[] wayPoints;
    private int currentWayPointIndex = -1;
    private Transform targetWayPoint;

    public override void OnEnter() {

        currentWayPointIndex = (currentWayPointIndex+1) % wayPoints.Length;
        targetWayPoint = wayPoints[currentWayPointIndex];

    }

    public override void OnUpdate() {
        if(canWalk)
        {
            npc.position = Vector2.MoveTowards(npc.position, targetWayPoint.position, Time.deltaTime * maxSpeed);

            if (Vector2.Distance(npc.position, targetWayPoint.position) <= stoppingDist)
            {
                owner.SwitchState(typeof(IdleState));
            }
        }
    }

    public override void OnExit() {}

}